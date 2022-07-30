using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models.Jorson.Blogs;
using Client.Services.Jorson;

namespace Client.Pages.Webring.Jorson.Blogs.Food
{
	internal class IndexBase : PageBase {}

    public partial class Index
    {
        #region Public

        #region Constructors
        public Index()
        {
            _base = new IndexBase();
        }
        #endregion

        #region Constants
        public static string UserId = "jorson";
        #endregion

        #endregion

        #region Protected

        #region Member Methods
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                _base.Initialize();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _foodBlogService.UserId = UserId;
            _blogs = await _foodBlogService.GetAsync("food/food.json");
        }
        #endregion

        #endregion

        #region Private

        #region Members
        private IEnumerable<FoodBlog> _blogs { get; set; }
        [Inject]
        private FoodBlogService _foodBlogService { get; set; }
        private PageBase _base { get; set; }
        #endregion

        #endregion
    }
}
