﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary.Limited;

namespace ClipSupporter
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Mutex名を決める ■■■ 必ずアプリケーション固有の文字列に変更すること！ ■■■
            string mutexName = "ClipSupporterI5EC";
            //Mutexオブジェクトを作成する
            bool createdNew;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, mutexName, out createdNew);

            //ミューテックスの初期所有権が付与されたか調べる
            if (createdNew == false)
            {
                //されなかった場合は、すでに起動していると判断して終了
                MessageBox.Show("多重起動はできません。");
                mutex.Close();
                return;
            }

            LimitedState MyLimitedState = LicenseLimited.CanUsed();
            if (MyLimitedState == LimitedState.Warning)
            {
                MessageBox.Show($"主がログアウトしました。本ツールの使用期限は({LicenseLimited.LimitedUseDate.ToShortDateString()})までとなります。");
            }
            else if (MyLimitedState == LimitedState.Limited)
            {
                MessageBox.Show($"使用期限日({LicenseLimited.LimitedUseDate.ToShortDateString()})を越えましたので使用不可となります。すみません・・・");
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClipSupporterForm());
            }
            finally
            {
                //ミューテックスを解放する
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}
