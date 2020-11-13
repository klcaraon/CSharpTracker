using CSharpTracker.Facebook.Model;
using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTracker.Facebook.Controller
{
    public class FacebookPage
    {
        readonly FacebookClient fbClient;
        public FacebookPage(FacebookClient client)
        {
            this.fbClient = client;
        }
        public PageModel GetPageProperty()
        {
            try
            {
                var pageProperty = "["+fbClient.Get("/me").ToString()+"]";
                List< PageModel> pageModel = JsonConvert.DeserializeObject<List<PageModel>>(pageProperty);
                foreach (var model in pageModel)
                {
                    return model;
                }

                return null;
            }
            catch (FacebookOAuthException ex)
            {
                return null;
            }
        }

        /*public void GetDailyReactions(string postId)
        {
            try
            {
                var postReactions = "[" + fbClient.Get(postId + "/insights?fields=values&metric=post_reactions_by_type_total").ToString() + "]";
                List<PostReactions> reactions = JsonConvert.DeserializeObject<List<PostReactions>>(postReactions);
                foreach (var item in reactions)
                {
                    foreach (var temp in item.data)
                    {
                        foreach (var items in temp.values)
                        {
                            return items.value;
                        }
                        return null;
                    }
                    return null;
                }
                return null;
            }
            catch (FacebookOAuthException ex)
            {
                var test = ex.ToString();
                return null;
            }
        }*/
    }
}
