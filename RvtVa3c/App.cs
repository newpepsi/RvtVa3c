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

      RibbonItemData i1 = new PushButtonData( "RvtVa3c_Command", "PCMES����", path, "RvtVa3c.Command" );

      i1.ToolTip = "ΪPCMES �������ӻ�ģ��";

      //p.AddStackedItems( i1, i2, i3 );

      p.AddItem( i1 );
    }

    public void AppDocumentOpened(object sender,DocumentOpenedEventArgs args)
    {
      Document doc = args.Document;
      using(Transaction trans = new Transaction(doc))
      {
        trans.Start("Edit address");
        TaskDialog.Show("��ģ��",doc.Title);
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
        //TaskDialog.Show("infomation ", "�¼�ִ����");
        //Todo ....
        trans.Commit();
      }
    }


    public Result OnStartup( UIControlledApplication a )
    {
      PopulatePanel( a.CreateRibbonPanel( "����������" ) );
         Debug.WriteLine(a.ControlledApplication.VersionNumber+","+a.ControlledApplication.VersionName);

      TaskDialog.Show("7777777", "�¼�ִ����");
      try
      {
        //ע���¼�
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
