using Client.Http;

namespace Client.Services
{
    public class GeneralBlogService : BlogService
    {
        public override string Id {
            get => "general";
        }

        public GeneralBlogService(LocalClient client) : base(client) {}
    }
}
