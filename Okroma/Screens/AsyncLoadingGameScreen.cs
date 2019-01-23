//using Microsoft.Xna.Framework.Content;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Okroma.Screens
//{
//    public abstract class AsyncLoadingGameScreen : GameScreen
//    {
//        ContentManager content;
//    List<Action<ContentManager>> contentToLoad = new List<Action<ContentManager>>();
//    public override void LoadContent()
//    {
//        var content = CreateContentManager();
//        foreach (var contentLoadAction in contentToLoad)
//        {
//            contentLoadAction(content);
//        }
//    }

//    public override void UnloadContent()
//    {
//        content.Unload();
//    }

//    public async Task AsyncLoadContent()
//    {
//        var content = CreateContentManager();
//        return await Task.Run(() =>
//            {
//                for (int i = 0; i < contentToLoad.Count; i++)
//                {

//                }
//            });
//    }
//}
//}
