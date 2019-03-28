using System.Collections.Generic;
using System.Web.Http;
using BashBook.BAL.Group;
using BashBook.Model.Global;
using BashBook.Model.Group;

namespace BashBook.API.Controllers.Group
{
    [Authorize]
    [RoutePrefix("api/Group")]
    public class GroupController : BaseController
    {
        readonly GroupOperation _group = new GroupOperation();

        [HttpGet]
        [Route("GetPreviewList/{userId}")]
        public List<GroupPreviewModel> GetPreviewList(int userId)
        {
            return _group.GetPreviewList(userId);
        }


        [HttpGet]
        [Route("GetForEdit/{groupId}")]
        public ManageGroupModel GetForEdit(int groupId)
        {
            return _group.GetForEdit(groupId);
        }

        [HttpPost]
        [Route("Add")]
        public int Add(ManageGroupModel model)
        {
            return _group.Add(model);
        }

        [HttpPost]
        [Route("Edit")]
        public bool Edit(ManageGroupModel model)
        {
            return _group.Edit(model);
        }

        [HttpPost]
        [Route("UpdateImage")]
        public bool UpdateImage(StringModel model)
        {
            return _group.UpdateImage(model);
        }

        [HttpPost]
        [Route("UpdateMessage")]
        public bool UpdateMessage(StringModel model)
        {
            return _group.UpdateMessage(model);
        }

        [HttpPost]
        [Route("UpdateTitle")]
        public bool UpdateTitle(StringModel model)
        {
            return _group.UpdateTitle(model);
        }
    }
}
