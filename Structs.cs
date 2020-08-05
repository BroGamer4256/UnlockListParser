using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

public struct DataStruct
{
	public uint arrarySize {get; set;}
	public uint offsetStart {get; set;}
	public string type {get; set;}
	public List<dynamic> Data(string file) 
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();

		int Length = HexRead.EntryLength(type);
		var DataList = new List<dynamic>();
		var tempHex = new List<string>();
		var hex = new List<int>();
		int tempCount = 0;
		var fs = new FileStream(file, FileMode.Open);

		for (int i = 0; i < arrarySize; i++)
		{

			fs.Seek(offsetStart + Length*4*DataList.Count, SeekOrigin.Begin);
			for (int x = 0; x < Length*4; x++)
			{
				tempCount++;
				tempHex.Add(string.Format("{0:X2}", fs.ReadByte()));
				if (tempCount == 4)
				{
					string temp = tempHex[3] + tempHex[2] + tempHex[1] + tempHex[0];
					hex.Add(Convert.ToInt32(temp, 16));
					tempCount = 0;
					tempHex.Clear();
				}
			}

			switch (type)
			{
				case "ModuleUnlock": 
					DataList.Add(new ModuleUnlockEntry() 
					{
						ModuleID = hex[0], 
						muUnk01 = hex[1], 
						muPvPID = hex[2], 
						muSCC = hex[3], 
						muUnk02 = hex[4], 
						muDiffClr = hex[5], 
						muRankClr = hex[6], 
						muUnk05 = hex[7], 
						muTCC = hex[8], 
						muUnk07 = hex[9], 
						muUnk08 = hex[10], 
						muUnk09 = hex[11]
					});
					break;
				case "PVUnlock": 
					DataList.Add(new PvUnlockEntry() 
					{
						suPVID = hex[0], 
						suUnk01 = hex[1], 
						suPVClr01 = hex[2],
						suPVClr02 = hex[3], 
						suPVClr03 = hex[4], 
						suPVClr04 = hex[5], 
						suPVClr05 = hex[6], 
						suPVClrDiff = hex[7], 
						suPVClrRank = hex[8], 
						suUnk02 = hex[9], 
						suUnk03 = hex[10], 
						suUnk04 = hex[11], 
						suUnk05 = hex[12], 
						suUnk06 = hex[13], 
						suUnk07 = hex[14], 
						suPVHighClrDiff = hex[15], 
						suPVHighClrRank = hex[16], 
						suUnk08 = hex[17]
					});
					break;
                case "CMNITMUnlock":
                    DataList.Add(new CmnUnlock() 
					{
						cuModID = hex[0], 
                        cuUnk01 = hex[1], 
                        cuPvPID = hex[2], 
                        cuSCC = hex[3], 
                        cuUnk02 = hex[4], 
                        cuDiffClr = hex[5], 
                        cuRankClr = hex[6], 
                        cuUnk05 = hex[7], 
                        cuTCC = hex[8], 
                        cuUnk07 = hex[9], 
                        cuUnk08 = hex[10], 
                        cuUnk09 = hex[11]
					});
                    break;
                case "VocaRoomUnlock":
                    DataList.Add(new VocaRoomUnlock()
                    {
                        bvVocaloid = hex[0],
                        bvSongId = hex[1],
                        bvScc = hex[2]
                    });
                    break;
				default: continue;
			}
			hex.Clear();
		}
		fs.Close();
		return DataList;
	}
}

public struct ModuleUnlockEntry
{
	public int ModuleID {get; set;}
	public int muUnk01 {get; set;}
	public int muPvPID {get; set;}
	public int muSCC {get; set;}
	public int muUnk02 {get; set;}
	public int muDiffClr {get; set;} // 0 for easy, 1 for normal, 2 for hard, 3 for extreme, 4 for any?
	public int muRankClr {get; set;} // 0 for missxtake, 1 for lousy, 2 for standard, 3 for great, 4 for excellent, 5 for perfect, 6 appears to be any rank
	public int muUnk05 {get; set;}
	public int muTCC {get; set;} // appears to take effect with a PV flag of -2, otherwise it's a clone of the song clear count.
	public int muUnk07 {get; set;}
	public int muUnk08 {get; set;}
	public int muUnk09 {get; set;}
}

public struct PvUnlockEntry
{
	public int suPVID {get; set;}
	public int suUnk01 {get; set;}
	public int suPVClr01 {get; set;}
	public int suPVClr02 {get; set;}
	public int suPVClr03 {get; set;}
	public int suPVClr04 {get; set;}
	public int suPVClr05 {get; set;}
	public int suPVClrDiff {get; set;}
	public int suPVClrRank {get; set;}
	public int suUnk02 {get; set;}
	public int suUnk03 {get; set;}
	public int suUnk04 {get; set;}
	public int suUnk05 {get; set;}
	public int suUnk06 {get; set;}
	public int suUnk07 {get; set;}
	public int suPVHighClrDiff {get; set;}
	public int suPVHighClrRank {get; set;}
	public int suUnk08 {get; set;}
}

public struct CmnUnlock
{
    public int cuModID {get; set;}
    public int cuUnk01 {get; set;}
    public int cuPvPID {get; set;}
    public int cuSCC {get; set;}
    public int cuUnk02 {get; set;}
    public int cuDiffClr {get; set;}
    public int cuRankClr {get; set;}
    public int cuUnk05 {get; set;}
    public int cuTCC {get; set;}
    public int cuUnk07 {get; set;}
    public int cuUnk08 {get; set;}
    public int cuUnk09 {get; set;}
}

public struct VocaRoomUnlock
{
    public int bvVocaloid {get; set;}
    public int bvSongId {get; set;}
    public int bvScc {get; set;}
}