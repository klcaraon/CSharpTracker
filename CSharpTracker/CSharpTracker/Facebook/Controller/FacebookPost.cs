using CSharpTracker.Facebook.Model;
using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CSharpTracker.Facebook.Controller
{
    public class FacebookPost
    {
        readonly FacebookClient fbClient;
        public FacebookPost(FacebookClient client)
        {
            this.fbClient = client;
        }
        public PostIdModel PostToWall(string pageId)
        {
            dynamic messagePost = new ExpandoObject();
            //messagePost.picture = @"C:\Users\Mac\Downloads\IMG_20191122_135736_1.jpg";
            //messagePost.link = "https://yaplex.com/";
            //messagePost.name = "This is a Name1";
            //messagePost.caption = "This is a caption1";
            //messagePost.description = "This is a description1";
            messagePost.message = DateTime.Now.ToString();
            try
            {
                string postId = "[" + fbClient.Post(pageId + "/feed", messagePost) + "]";

                List<PostIdModel> temp = JsonConvert.DeserializeObject<List<PostIdModel>>(postId);
                foreach (PostIdModel item in temp)
                {
                    return item;
                }
                return null;

            }
            catch (FacebookOAuthException ex)
            { //handle oauth exception } catch (FacebookApiException ex) { //handle facebook exception
                return null;
            }
        }
        public PostReactions.Value2 GetPostReactions(string postId)
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
            catch (FacebookOAuthException ex)            {
                var test = ex.ToString();
                return null;
            }
        }
        public void GetPostComments(string postId)
        {
            //103967058170662_105535241347177?fields=comments
            //dynamic messagePost = new ExpandoObject();
            //messagePost.message = "This is a message1";
            List<CommentsModel> commentLists = new List<CommentsModel>();
            try
            {
                string test = "[" + fbClient.Get(postId + "?fields=comments").ToString() + "]";
                List<RetrievedCommentsModel> token = JsonConvert.DeserializeObject<List<RetrievedCommentsModel>>(test);
                foreach (RetrievedCommentsModel item in token)
                {
                    foreach (var items in item.comments.data)
                    {
                        commentLists.Add(new CommentsModel
                        {
                            id = items.id,
                            created_time = items.created_time.ToString(),
                            message = items.message
                        });
                    }
                }
            }
            catch (FacebookOAuthException ex)
            {

            }
        }
        public void UpdatePost(string pageId)
        {
            dynamic messagePost = new ExpandoObject();
            //messagePost.picture = @"C:\Users\Mac\Downloads\IMG_20191122_135736_1.jpg";
            //messagePost.link = "https://yaplex.com/";
            //messagePost.name = "This is a Name1";
            //messagePost.caption = "This is a caption1";
            //messagePost.description = "This is a description1";
            messagePost.message = DateTime.Now.ToString();
            try
            {
                string postId = "[" + fbClient.Post(pageId + "/feed", messagePost) + "]";

                List<PostIdModel> temp = JsonConvert.DeserializeObject<List<PostIdModel>>(postId);
                foreach (PostIdModel item in temp)
                {

                }
            }
            catch (FacebookOAuthException ex)
            { //handle oauth exception } catch (FacebookApiException ex) { //handle facebook exception

            }
        }
        public void UploadPhoto(string pageId)
        {
            dynamic messagePost = new ExpandoObject();
            //messagePost.source = @"C:\Users\Mac\Downloads\Salang_Pass_Tunnel.jpg";
            messagePost.url = "https://appharbor.com/assets/images/stackoverflow-logo.png";
            //messagePost.link = "https://yaplex.com/";
            //messagePost.name = "This is a Name1";
            //messagePost.caption = "This is a caption1";
            //messagePost.description = "This is a description1";
            messagePost.message = DateTime.Now.ToString();
            try
            {
                string postId = "[" + fbClient.Post(pageId + "/photos", messagePost) + "]";

                List<PostIdModel> temp = JsonConvert.DeserializeObject<List<PostIdModel>>(postId);
                foreach (PostIdModel item in temp)
                {

                }
            }
            catch (FacebookOAuthException ex)
            { //handle oauth exception } catch (FacebookApiException ex) { //handle facebook exception
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
