using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Client.Services.Jorson;
using Client.Http;
using Client.Models.Jorson.Blogs;

namespace Client.Pages.Blogs.General
{
    public partial class BlogPage
    {
        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _blog = await _blogService.GetByIdAsync(Id);
            if (_blog?.Content is null)
            {
                _navigation.NavigateTo("/blogs/general");
                return;
            }
            _blog.Id = Id;
            if (!string.IsNullOrWhiteSpace(_blog.Content.Path))
            {
                _blog.Content.Text = await _localClient.Client.GetStringAsync($"/data/{UserId}/{_blog.Content.Path}");
            }
        }

        private Blog _blog { get; set; }
        [Inject]
        private GeneralBlogService _blogService { get; set; }
        [Inject]
        private LocalClient _localClient { get; set; }
        [Inject]
        private NavigationManager _navigation { get; set; }
        [Inject]
        private IJSRuntime _jsRuntime { get; set; }
    }
}
