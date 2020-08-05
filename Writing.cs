using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class HexWrite
{
    public static void Write()
    {
        CMNData();
        VocaData();
    }
    public static void VocaData()
    {
        var doc = new XmlDocument();
		doc.Load(@"unlock_list\\VocaRoomUnlock.xml");
        var VocaRoomReadNode = new List<dynamic>();
		var VocaRoomReadData = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			VocaRoomReadNode.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < VocaRoomReadNode.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = VocaRoomReadNode[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new VocaRoomUnlock() 
			{
				bvVocaloid = Convert.ToInt32(xmlDataSplit[2]),
                bvSongId = Convert.ToInt32(xmlDataSplit[6]),
                bvScc = Convert.ToInt32(xmlDataSplit[10])
			};
            VocaRoomReadData.Add(TBA); 
		}
    }
    public static void CMNData()
    {
        var doc = new XmlDocument();
		doc.Load(@"unlock_list\\CMNITMUnlock.xml");
		var CMNReadNode = new List<dynamic>();
		var CMNReadData = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			CMNReadNode.Add(node);
		}
		string[] xmlData = {};
		for (int i = 0; i < CMNReadNode.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = CMNReadNode[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
			var TBA = new CmnUnlock() 
			{
				cuModID = Convert.ToInt32(xmlDataSplit[2]),
				cuUnk01 = Convert.ToInt32(xmlDataSplit[6]), 
				cuPvPID = Convert.ToInt32(xmlDataSplit[10]), 
				cuSCC = Convert.ToInt32(xmlDataSplit[14]),
				cuUnk02 = Convert.ToInt32(xmlDataSplit[18]),
				cuDiffClr = Convert.ToInt32(xmlDataSplit[22]), 
				cuRankClr = Convert.ToInt32(xmlDataSplit[26]), 
				cuUnk05 = Convert.ToInt32(xmlDataSplit[30]), 
				cuTCC = Convert.ToInt32(xmlDataSplit[34]), 
				cuUnk07 = Convert.ToInt32(xmlDataSplit[38]), 
				cuUnk08 = Convert.ToInt32(xmlDataSplit[42]), 
				cuUnk09 = Convert.ToInt32(xmlDataSplit[46])
			};
            CMNReadData.Add(TBA);
		}
    }
}