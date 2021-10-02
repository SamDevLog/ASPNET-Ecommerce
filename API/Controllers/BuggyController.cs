using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class BuggyController : BaseApiController {
        private readonly StoreContext context;

        public BuggyController (StoreContext context) {
            this.context = context;
        }

        [HttpGet ("notfound")]
        public ActionResult GetNotFoundResult () {
            var thing = context.Products.Find (42);
            if (thing == null) {

                return NotFound (new ApiResponse (404));
            }
            return Ok ();
        }

        [HttpGet ("server")]
        public ActionResult GetServerResult () {

            var thing = context.Products.Find (42);

            var thingToReturn = thing.ToString ();

            return Ok ();
        }

        [HttpGet ("badrequest")]
        public ActionResult GetBadRequestResult () {
            return BadRequest (new ApiResponse (400));
        }

        [HttpGet ("badrequest/{id}")]
        public ActionResult GetNotFoundResult (int id) {
            return Ok ();
        }
    }
}