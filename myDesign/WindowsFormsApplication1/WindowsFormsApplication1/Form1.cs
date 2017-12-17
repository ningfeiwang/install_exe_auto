using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Threading;
using System.Windows.Automation;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        AutomationElement calWindow = null;//计算器窗口主窗口元素
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll ", EntryPoint = "PostMessage")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private void OnWindowOpenOrClose(object sender, AutomationEventArgs e)
        {
            if (calWindow != null)
                return;
            if (e.EventId != WindowPattern.WindowOpenedEvent)
            {
                return;
            }
            if (sender == null)
            {
                Console.WriteLine("sender is null");
                return;
            }
            Thread.Sleep(1000);//此处必须等待一下，应该是计算器的等待计算器完全加载，不然控件 找不到
            AutomationElement sourceElement = null;
            sourceElement = sender as AutomationElement;
            Console.WriteLine(sourceElement.Current.Name);
            try
            {
                sourceElement = sender as AutomationElement;
                Console.WriteLine(sourceElement.Current.Name);
                if (sourceElement.Current.Name == "AutoIt v3.3.0.0 Setup")
                {
                    calWindow = sourceElement;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex:" + ex.Message);
                return;
            }
            if (calWindow == null)
            {
                return;
            }
            ExcuteTest();
        }
        private void ExcuteTest()
        {
            ExcuteButtonInvoke(@"Next >");
            ExcuteButtonInvoke(@"I Agree");
           
            ExcuteButtonInvoke(@"Next >");
            
            ExcuteButtonInvoke(@"Next >");
            ExcuteButtonInvoke(@"Next >");
            ExcuteButtonInvoke(@"Cancel");
            //ExcuteButtonInvoke(btnPlusAutoID);
            //ExcuteButtonInvoke(btn3AutoID);
            //ExcuteButtonInvoke(btnEqualAutoID);
            //string rs = GetCurrentResult();
            //Console.WriteLine(rs);
        }
        const uint WM_LBUTTONDOWN = 0x0201;//表示按下鼠标左键
        const uint WM_LBUTTONUP = 0x0202;//表示鼠标左键抬起
        public AutomationElement AppgAE(IntPtr AppFormHandle)
        {
            //获取待测程序主窗体的AutomationElement
            AutomationElement AppAE = AutomationElement.FromHandle(AppFormHandle);
            if (AppAE == null)
            {
                new Exception("获取待测程序窗体的AutomationElement 失败");
            }
            return AppAE;
        }//AppAE()
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        extern static bool SetCursorPos(int x, int y);

        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        [DllImport("user32.dll",EntryPoint= "mouse_event")]
        extern static void mouse_event(uint dwFlags, int dX, int dY, uint dwData, IntPtr dwExtralnfo);
        public void MouseLeftClick(  AutomationElement TargetAE, int ClickCount)
        {
            Rect TargetRect = TargetAE.Current.BoundingRectangle;
            int dX = (int)(TargetRect.Left + TargetRect.Width / 2);//目标控件的X坐标+目标控件的宽度/2
            int dY = (int)(TargetRect.Top + TargetRect.Height / 2);//目标控件的Y坐标+目标控件的高度/2

            //将鼠标移至控件中心(dx,dY)
            SetCursorPos(dX, dY);

            int I = 0;
            do
            {
                Thread.Sleep(250);
                mouse_event(MOUSEEVENTF_LEFTDOWN, dX, dY, 0, IntPtr.Zero);//鼠标左键按下
                mouse_event(MOUSEEVENTF_LEFTUP, dX, dY, 0, IntPtr.Zero);//鼠标左键抬起
                I = I + 1;
            } while (I < ClickCount);//ClickCount是指点击次数，我们可以通过控制循环次数来控制点击次数
        }//MouseLeftClick()
        [DllImport("user32.dll ", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindow_AppFormHandle(string lpClassName, string lpWindowName);

        /// <summary>
        /// 通过WinAPI，获取待测程序主窗体的句柄
        /// </summary>
        /// <param name="AppFormClassName">待测程序主窗体类名</param>
        /// <param name="AppFormWindowName">待测程序主窗体名</param>
        /// <returns></returns>
        public IntPtr AppFormHandle(string AppFormClassName, string AppFormWindowName)
        {
            IntPtr AppFormHandle = IntPtr.Zero;
            bool FormFound = false;
            int Attempts = 0;
            do
            {
                if (AppFormHandle == IntPtr.Zero)
                {
                    Thread.Sleep(100);
                    Attempts = Attempts + 1;
                    AppFormHandle = FindWindow_AppFormHandle(AppFormClassName, AppFormWindowName);
                }
                else
                {
                    FormFound = true;
                }
            } while (!FormFound && Attempts < 99);//为防止因FindWindow无法遍历到待测主窗体，而陷入无限循环之中，因此增加一个遍历次数Attempts < 99的条件
            if (AppFormHandle == IntPtr.Zero)
            {
                new Exception("获取待测程序主窗体 失败");
            }
            return AppFormHandle;

        }//AppFormHandle()
        private void ExcuteButtonInvoke(string automationId)
        {
            Condition conditions = new AndCondition(
                new PropertyCondition(AutomationElement.NameProperty, automationId),
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));
            if (calWindow == null)
                return;
            PropertyCondition Condition = new PropertyCondition(AutomationElement.NameProperty, automationId);
            AutomationElement btn = calWindow.FindFirst(TreeScope.Descendants, Condition);
            if (btn == null)
                return;
            MouseLeftClick(btn, 1);
            //if (btn != null)
            //{
            //    InvokePattern invokeptn = (InvokePattern)btn.GetCurrentPattern(InvokePattern.Pattern);
            //    invokeptn.Invoke();
            //}
            Thread.Sleep(1000);
        }
        public static void DeployApplications(string executableFilePath)
        {
            PowerShell powerShell = null;
            Console.WriteLine(" ");
            Console.WriteLine("Deploying application...");
            try
            {
                using (powerShell = PowerShell.Create())
                {
                    // 'C:\\ApplicationRepository\\FileZilla_3.14.1_win64-setup.exe'”

                    powerShell.AddScript("$setup=Start-Process '"+ executableFilePath + "'");

                    Collection<PSObject> PSOutput = powerShell.Invoke();
                    foreach (PSObject outputItem in PSOutput)
                    {

                        if (outputItem != null)
                        {

                            Console.WriteLine(outputItem.BaseObject.GetType().FullName);
                            Console.WriteLine(outputItem.BaseObject.ToString() + "\n");
                        }
                    }

                    if (powerShell.Streams.Error.Count > 0)
                    {
                        string temp = powerShell.Streams.Error.First().ToString();
                        Console.WriteLine("Error: {0}", temp);

                    }
                    else
                        Console.WriteLine("Installation has completed successfully.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: {0}", ex.InnerException);
                //throw;
            }
            finally
            {
                if (powerShell != null)
                    powerShell.Dispose();
            }

        }
        private void bt_run_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtPath.Text))
             {
                // DeployApplications(txtPath.Text);
                // Process process = Process.Start(txtPath.Text);

                // int processId = process.Id;

                 AutomationEventHandler eventHandler = new AutomationEventHandler(OnWindowOpenOrClose);
                 Automation.AddAutomationEventHandler(WindowPattern.WindowOpenedEvent, AutomationElement.RootElement, TreeScope.Children, eventHandler);
                 Process.Start(@"C:\Users\woaixj\Desktop\ui\Install_UnInstall_AutoIt\autoit-v3-setup.exe");
             }
            else
            {
                MessageBox.Show("无此文件");
            }
        }

        private void bt_brower_Click(object sender, EventArgs e)
        {
            ofdMsiBrowser.InitialDirectory = @"C:\";
            ofdMsiBrowser.Filter = "exe installer files (*.exe)|*.exe";
            if (ofdMsiBrowser.ShowDialog() == DialogResult.OK)
                txtPath.Text = ofdMsiBrowser.FileName;
        }
    }
}
