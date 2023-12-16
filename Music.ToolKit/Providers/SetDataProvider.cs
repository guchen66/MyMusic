
using System.Collections.Generic;
using System.Linq;


namespace Music.ToolKit.Providers
{
    public static class SetDataProvider
    {
        /*public static MySetData GetMySetData(this List<MySetData> myDatas, string key)
        {
           return myDatas.Where(it => it.Key == key).SingleOrDefault();       //通过key值获取List信息
        }

        /// <summary>
        /// 通过类型获取Id
        /// </summary>
        /// <param name="myDatas"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetId(this List<MySetData> myDatas,MySetType stateInfo,string key)
        {
            var myData = myDatas.GetMySetData(key).ValueType;
            if (stateInfo == myData )
            {
                // 根据 SomeStateInfo1 类型获取相应的 Id
               // return stateInfo.Toint();
            }
           
            // 处理其他情况，可以返回默认值或抛出异常

            return 0; // 或者根据具体逻辑返回适当的默认值
        }

        private static bool GetIsSelected(object stateInfo)
        {

           *//* if (stateInfo is CommonSetInfo stateInfo1)
            {
                // 根据 SomeStateInfo1 类型获取相应的 IsSelected 值
                return stateInfo1.IsSelected;
            }
            else if (stateInfo is DownLoadInfo stateInfo2)
            {
                // 根据 SomeStateInfo2 类型获取相应的 IsSelected 值
                return stateInfo2.IsSelected;
            }*//*
            // 处理其他情况，可以返回默认值或抛出异常

            return false; // 或者根据具体逻辑返回适当的默认值
        }
        public static void SetId(this List<MySetData> mySetDatas, string result)
        {
            MySetData mySetData= mySetDatas.GetMySetData("Id");

            if (mySetData!=null)
            {
                mySetData.ValueData=result;
            }
            else 
            {
                mySetDatas.Add(new MySetData("Id",MySetData.MySetType.CommonSetInfo,result));
            }
        }*/

    }
}
