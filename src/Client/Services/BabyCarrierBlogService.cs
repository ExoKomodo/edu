using Client.Http;

namespace Client.Services
{
    public class BabyCarrierBlogService : BlogService
    {
        public override string Id {
            get => "baby-carrier";
        }

        public BabyCarrierBlogService(LocalClient client) : base(client) {}
    }
}
