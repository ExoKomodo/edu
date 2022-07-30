using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models.Jorson.Blogs;
using Client.Services.Jorson;

namespace Client.Pages.Blogs.General
{
    public partial class Index
    {
        protected override async Task OnInitializedAsync()
        {
            _blogs = await _blogService.GetAsync();
        }

        private IEnumerable<Blog> _blogs { get; set; }
        [Inject]
        private GeneralBlogService _blogService { get; set; }
    }
}
