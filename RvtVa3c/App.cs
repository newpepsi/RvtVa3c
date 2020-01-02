#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Diagnostics;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI.Events;
using System.Windows.Forms;
#endregion

namespace RvtVa3c
{
  class App : IExternalApplication
  {
    public string savedPath;
    /// <summary>
    /// Add buttons for our command
    /// to the ribbon panel.
    /// </summary>
    void PopulatePanel( RibbonPanel p )
    {
      string path = Assembly.GetExecutingAssembly().Location;

      RibbonItemData i1 = new PushButtonData( "RvtVa3c_Command", "PCMES导出", path, "RvtVa3c.Command" );

      i1.ToolTip = "为PCMES 导出可视化模型";

      //p.AddStackedItems( i1, i2, i3 );

      p.AddItem( i1 );
    }

    public void AppDocumentOpened(object sender,DocumentOpenedEventArgs args)
    {
      Document doc = args.Document;
      using(Transaction trans = new Transaction(doc))
      {
        trans.Start("Edit address");
        TaskDialog.Show("打开模型",doc.Title);
        //Todo ....
        trans.Commit();
      }
    }
    public void ViewActivated(object sender, ViewActivatedEventArgs args)
    {
      Document doc = args.Document;
      using (Transaction trans = new Transaction(doc)) 
      {
        Command command = new Command();
        trans.Start("Edit address");
        //command.Execute()
        //TaskDialog.Show("infomation ", "事件执行中");
        //Todo ....
        trans.Commit();
      }
    }


    public Result OnStartup( UIControlledApplication a )
    {
      PopulatePanel( a.CreateRibbonPanel( "轻量化导出" ) );
         Debug.WriteLine(a.ControlledApplication.VersionNumber+","+a.ControlledApplication.VersionName);

      TaskDialog.Show("7777777", "事件执行中");
      try
      {
        //注册事件
        a.ControlledApplication.DocumentOpened +=
          new EventHandler<DocumentOpenedEventArgs>(AppDocumentOpened);
        a.ViewActivated += new EventHandler<ViewActivatedEventArgs>(ViewActivated);
      }
      catch (Exception)
      {
      }
      return Result.Succeeded;
    }

    public Result OnShutdown( UIControlledApplication a )
    {

      a.ControlledApplication.DocumentOpened -= AppDocumentOpened;
      return Result.Succeeded;
    }
  }
}
