#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Diagnostics;
#endregion

namespace RvtVa3c
{
  class App : IExternalApplication
  {
    /// <summary>
    /// Add buttons for our command
    /// to the ribbon panel.
    /// </summary>
    void PopulatePanel( RibbonPanel p )
    {
      string path = Assembly.GetExecutingAssembly().Location;

      RibbonItemData i1 = new PushButtonData( "RvtVa3c_Command", "vA3C \r\n Export", path, "RvtVa3c.Command" );

      i1.ToolTip = "Export three.js JSON objects for vA3C AEC viewer";

      //p.AddStackedItems( i1, i2, i3 );

      p.AddItem( i1 );
    }

    public Result OnStartup( UIControlledApplication a )
    {
      PopulatePanel( a.CreateRibbonPanel( "vA3C Export" ) );
      TaskDialog td = new TaskDialog("输入序列号来启用插件");
      td.Title = "请输入序列号";
      td.CommonButtons = TaskDialogCommonButtons.No | TaskDialogCommonButtons.Yes;
      td.MainInstruction = "你需要对导出的内容进行过滤么？";
      td.MainContent = "点击是以后你将在弹窗的窗口中过滤每个类别的内容";
      td.AllowCancellation = true;
      td.VerificationText = "勾选此处连同类型属性一同导出";
      if(TaskDialogResult.Yes == td.Show())
      {
        Debug.WriteLine("点击了确定");
      }
      Debug.WriteLine(a.ControlledApplication.VersionNumber+","+a.ControlledApplication.VersionName);
      return Result.Succeeded;
    }

    public Result OnShutdown( UIControlledApplication a )
    {
      return Result.Succeeded;
    }
  }
}
