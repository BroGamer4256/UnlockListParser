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
				case "RoomThemeUnlock":
					DataList.Add(new RoomThemeUnlock()
					{
						rtThemeID = hex[0],
						rtUnk01 = hex[1],
						rtUnlkFlag = hex[2],
						rtUnk02 = hex[3],
						rtUnk03 = hex[4],
						rtLwDiff = hex[5],
						rtLwRank = hex[6],
						rtUnk04 = hex[7],
						rtUnk05 = hex[8],
						rtUnk06 = hex[9],
						rtUnk07 = hex[10],
						rtHgDiff = hex[11],
						rtHgRank = hex[12],
						rtUnkEnd = hex[13]
					});
					break;
				case "RoomPartsUnlock":
					DataList.Add(new RoomPartsUnlock()
					{
						rpThemeID = hex[0],
						rpUnk01 = hex[1],
						rpUnlkFlag = hex[2],
						rpUnk02 = hex[3],
						rpUnk03 = hex[4],
						rpLwDiff = hex[5],
						rpLwRank = hex[6],
						rpUnk04 = hex[7],
						rpUnk05 = hex[8],
						rpUnk06 = hex[9],
						rpUnk07 = hex[10],
						rpHgDiff = hex[11],
						rpHgRank = hex[12],
						rpUnkEnd = hex[13]
					});
					break;
				case "RoomItemUnlock":
					DataList.Add(new RoomItemUnlock()
					{
						riItemID = hex[0],
						riUnk01 = hex[1],
						riUnlkReq = hex[2],
						riUnk02 = hex[3],
						riUnk03 = hex[4],
						riLwClearDiff = hex[5],
						riLwClearRank = hex[6],
						riUnk04 = hex[7],
						riUnk05 = hex[8],
						riUnk06 = hex[9],
						riUnk07 = hex[10],
						riUnk08 = hex[11],
						riUnk09 = hex[12],
						riHgClearDiff = hex[13],
						riHgClearRank = hex[14],
						riUnk10 = hex[15],
						riUnk11 = hex[16],
						riUnk12 = hex[17]
					});
					break;
				case "GiftItemUnlock":
					DataList.Add(new GiftItemUnlock()
					{
						giID = hex[0],
						giUnk01 = hex[1],
						giFlag = hex[2],
						giUnk02 = hex[3],
						giUnk03 = hex[4],
						giLwClrDiff = hex[5],
						giLwClrRank = hex[6],
						giUnk04 = hex[7],
						giUnk05 = hex[8],
						giUnk06 = hex[9],
						giUnk07 = hex[10],
						giUnk08 = hex[11],
						giHgClrDiff = hex[12],
						giHgClrRank = hex[13],
						giUnk09 = hex[14],
						giUnk10 = hex[15]
					});
					break;
				case "PVTitleUnlock":
					DataList.Add(new PVTitleUnlock()
					{
						ptID = hex[0],
						ptUnk01 = hex[1],
						ptUnk02 = hex[2],
						ptUnk03 = hex[3],
						ptUnk04 = hex[4],
						ptUnk05 = hex[5],
						ptUnk06 = hex[6],
						ptUnk07 = hex[7],
						ptUnk08 = hex[8]
					});
					break;
				case "RoomTitleUnlock":
					DataList.Add(new RoomTitleUnlock()
					{
						rtID = hex[0],
						rtUnk01 = hex[1],
						rtUnk02 = hex[2],
						rtUnk03 = hex[3]
					});
					break;
				case "EditTitleUnlock":
					DataList.Add(new EditTitleUnlock()
					{
						etID = hex[0],
						etUnk01 = hex[1],
						etUnk02 = hex[2],
						etUnk03 = hex[3]
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
	public int muDiffClr {get; set;}
	public int muRankClr {get; set;}
	public int muUnk05 {get; set;}
	public int muTCC {get; set;}
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

public struct RoomThemeUnlock
{
	public int rtThemeID {get; set;}
	public int rtUnk01 {get; set;}
	public int rtUnlkFlag {get; set;}
	public int rtUnk02 {get; set;}
	public int rtUnk03 {get; set;}
	public int rtLwDiff {get; set;}
	public int rtLwRank {get; set;}
	public int rtUnk04 {get; set;}
	public int rtUnk05 {get; set;}
	public int rtUnk06 {get; set;}
	public int rtUnk07 {get; set;}
	public int rtHgDiff {get; set;}
	public int rtHgRank {get; set;}
	public int rtUnkEnd {get; set;}
}

public struct RoomPartsUnlock
{
	public int rpThemeID {get; set;}
	public int rpUnk01 {get; set;}
	public int rpUnlkFlag {get; set;}
	public int rpUnk02 {get; set;}
	public int rpUnk03 {get; set;}
	public int rpLwDiff {get; set;}
	public int rpLwRank {get; set;}
	public int rpUnk04 {get; set;}
	public int rpUnk05 {get; set;}
	public int rpUnk06 {get; set;}
	public int rpUnk07 {get; set;}
	public int rpHgDiff {get; set;}
	public int rpHgRank {get; set;}
	public int rpUnkEnd {get; set;}
}

public struct RoomItemUnlock
{
	public int riItemID {get; set;}
	public int riUnk01 {get; set;}
	public int riUnlkReq {get; set;}
	public int riUnk02 {get; set;}
	public int riUnk03 {get; set;}
	public int riLwClearDiff {get; set;}
	public int riLwClearRank {get; set;}
	public int riUnk04 {get; set;}
	public int riUnk05 {get; set;}
	public int riUnk06 {get; set;}
	public int riUnk07 {get; set;}
	public int riUnk08 {get; set;}
	public int riUnk09 {get; set;}
	public int riHgClearDiff {get; set;}
	public int riHgClearRank {get; set;}
	public int riUnk10 {get; set;}
	public int riUnk11 {get; set;}
	public int riUnk12 {get; set;}
}

public struct GiftItemUnlock
{
	public int giID {get; set;}
	public int giUnk01 {get; set;}
	public int giFlag {get; set;}
	public int giUnk02 {get; set;}
	public int giUnk03 {get; set;}
	public int giLwClrDiff {get; set;}
	public int giLwClrRank {get; set;}
	public int giUnk04 {get; set;}
	public int giUnk05 {get; set;}
	public int giUnk06 {get; set;}
	public int giUnk07 {get; set;}
	public int giUnk08 {get; set;}
	public int giHgClrDiff {get; set;}
	public int giHgClrRank {get; set;}
	public int giUnk09 {get; set;}
	public int giUnk10 {get; set;}
}

public struct PVTitleUnlock
{
	public int ptID {get; set;}
	public int ptUnk01 {get; set;}
	public int ptUnk02 {get; set;}
	public int ptUnk03 {get; set;}
	public int ptUnk04 {get; set;}
	public int ptUnk05 {get; set;}
	public int ptUnk06 {get; set;}
	public int ptUnk07 {get; set;}
	public int ptUnk08 {get; set;}
}

public struct RoomTitleUnlock
{
	public int rtID {get; set;}
	public int rtUnk01 {get; set;}
	public int rtUnk02 {get; set;}
	public int rtUnk03 {get; set;}
}

public struct EditTitleUnlock
{
	public int etID {get; set;}
	public int etUnk01 {get; set;}
	public int etUnk02 {get; set;}
	public int etUnk03 {get; set;}
}