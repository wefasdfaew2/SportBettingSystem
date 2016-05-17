namespace SportBettingSystem.Data.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Transactions;
    using System.Xml.Linq;

    using Models;
    using RepoFactory;
    using Repositories;

    public class DatabaseHelper
    {
        private static DatabaseHelper instance = null;
        private static object lockThis = new object();
        private static Timer timer;

        public DatabaseHelper()
        {
        }

        public static DatabaseHelper GetInstance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                        instance = new DatabaseHelper();

                    return instance;
                }
            }
        }

        public void Populate()
        {
            timer = new Timer(
                (e) =>
                {
                    using (var transaction = new TransactionScope())
                    {
                        XmlToEntitiesParser();
                        transaction.Complete();
                    }
                },
                null,
                0,
                (int)TimeSpan.FromSeconds(60).TotalMilliseconds);
        }

        private void XmlToEntitiesParser()
        {
            var matchRepo = new RepoFactory().Get<MatchRepository>();

            XDocument doc = XDocument.Load(@"http://vitalbet.net/sportxml");
            //XDocument doc = XDocument.Load("C:/Users/Geogi Malkovski/Downloads/JOBS/UltraPlay/SportBettingSystem/Server/SportBettingSystem.Api/App_Data/sportxml.xml");
            var matches = doc.Descendants("Match");

            foreach (var match in matches)
            {
                var matchEntity = this.SaveMatch(match, matchRepo);

                var currentMatchBets = match.Nodes();

                if (currentMatchBets.Any())
                {
                    foreach (var bet in currentMatchBets)
                    {
                        var item = (XElement)bet;
                        var betEntity = this.SaveBet(item, matchEntity);

                        var currentBetOdds = item.Nodes();

                        if (currentBetOdds.Any())
                        {
                            foreach (var odd in currentBetOdds)
                            {
                                var element = (XElement)odd;
                                this.SaveOdd(element, betEntity, matchEntity);
                            }
                        }
                    }
                }
            }

            matchRepo.SaveChanges();
        }

        private void SaveOdd(XElement element, Bet bet, Match match)
        {
            var number = (string)element.Attribute("ID");
            var name = (string)element.Attribute("Name");
            var value = float.Parse((string)element.Attribute("Value"), CultureInfo.InvariantCulture);
            var specialBetValue = (string)element.Attribute("SpecialBetValue") != null
                                        ? float.Parse((string)element.Attribute("SpecialBetValue"), CultureInfo.InvariantCulture)
                                        : (float?)null;

            var odd = bet.Odds.FirstOrDefault(b => b.Number == number);

            if (odd != null)
            {
                if (odd.Name != name || odd.Value != value || odd.SpecialBetValue != specialBetValue)
                {
                    odd.Name = name;
                    odd.Value = value;
                    odd.SpecialBetValue = specialBetValue;
                    match.ModifiedOn = DateTime.Now;
                }
            }
            else
            {
                odd = new Odd();

                odd.Number = number;
                odd.Name = name;
                odd.Value = value;
                odd.SpecialBetValue = specialBetValue;

                bet.Odds.Add(odd);
                match.ModifiedOn = DateTime.Now;
            }
        }

        private Bet SaveBet(XElement element, Match match)
        {
            var number = (string)element.Attribute("ID");
            var name = (string)element.Attribute("Name");
            var isLive = (string)element.Attribute("IsLive") == "true" ? true : false;

            var bet = match.Bets.FirstOrDefault(b => b.Number == number);

            if (bet != null)
            {
                bool hasUpdates = false;

                if (bet.Name != name || bet.IsLive != isLive)
                {
                    bet.Name = name;
                    bet.IsLive = isLive;
                    hasUpdates = true;
                }

                var currentBetOdds = bet.Odds.Where(x => !x.IsDeleted).ToList();
                var odds = element.Descendants("Odd").Attributes("ID").Select(x => (string)x);

                for (int i = 0; i < currentBetOdds.Count; i++)
                {
                    var item = currentBetOdds[i];

                    if (!odds.Contains(item.Number))
                    {
                        item.IsDeleted = true;
                        item.DeletedOn = DateTime.Now;
                        hasUpdates = true;
                    }
                }

                if (hasUpdates)
                {
                    match.ModifiedOn = DateTime.Now;
                }
            }
            else
            {
                bet = new Bet();

                bet.Number = number;
                bet.Name = name;
                bet.IsLive = isLive;

                match.Bets.Add(bet);
                match.ModifiedOn = DateTime.Now;
            }

            return bet;
        }

        private Match SaveMatch(XElement element, MatchRepository matchRepo)
        {
            var sportName = (string)element.Parent.Parent.Attribute("Name");
            var eventName = (string)element.Parent.Attribute("Name");
            var number = (string)element.Attribute("ID");
            var name = (string)element.Attribute("Name");
            var startDate = DateTime.Parse((string)element.Attribute("StartDate"), CultureInfo.InvariantCulture);
            var matchType = (string)element.Attribute("MatchType");

            var match = matchRepo.GetByNumber(number);

            if (match != null)
            {
                bool hasUpdates = false;

                if (match.Name != name || match.StartDate != startDate || match.MatchType != matchType)
                {
                    match.Name = name;
                    match.StartDate = startDate;
                    match.MatchType = matchType;
                }

                var currentMatchBets = match.Bets.Where(x => !x.IsDeleted).ToList();
                var bets = element.Descendants("Bet").Attributes("ID").Select(x => (string)x);

                for (int i = 0; i < currentMatchBets.Count; i++)
                {
                    var item = currentMatchBets[i];

                    if (!bets.Contains(item.Number))
                    {
                        item.IsDeleted = true;
                        item.DeletedOn = DateTime.Now;
                        hasUpdates = true;
                    }
                }

                if (hasUpdates)
                {
                    match.ModifiedOn = DateTime.Now;
                }
            }
            else
            {
                match = new Match();

                match.Number = number;
                match.Name = name;
                match.Sport = sportName;
                match.Event = eventName;
                match.StartDate = startDate;
                match.MatchType = matchType;

                matchRepo.Add(match);
            }

            return match;
        }
    }
}
