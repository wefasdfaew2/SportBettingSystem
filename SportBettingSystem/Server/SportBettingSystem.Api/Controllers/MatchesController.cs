namespace SportBettingSystem.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Models.Matches;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/matches")]
    public class MatchesController : BaseController
    {
        public IHttpActionResult Get(int page = 1, int pageSize = 3)
        {
            var model = this.LoadModel<MatchModel>();
            model.GetTodayMatches(page, pageSize);
            return this.Ok(model);
        }
        
        [HttpGet]
        [Route("update")]
        public IHttpActionResult TakeUpdated(int page = 1, int pageSize = 3)
        {
            var model = this.LoadModel<MatchModel>();
            var result = model.GetUpdatedMatches(page, pageSize);
            return this.Ok(result);
        }
    }
}
