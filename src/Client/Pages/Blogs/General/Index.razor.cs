using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;
using Client.Services;

namespace Client.Pages.Blogs.General
{
    public partial class Index
    {
        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                _blogService.Id = "general";
                _blogs = await _blogService.GetAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private IEnumerable<Blog> _blogs { get; set; }
        [Inject]
        private BlogService _blogService { get; set; }
    }
}
