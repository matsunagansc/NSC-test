using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace namespacewrite{
    public class NSwriter{  

        //目的：指定領域内(今回は同じディレクトリ)のcsファイルに含まれている、
        // "namespace:対象のcsファイル" を取得しグローバル宣言させる

        static List<string> name = new();
        static string path = Directory.GetCurrentDirectory()+@"\HelloWorld\"; //任意のパス（同じディレクトリ）
        static Encoding enc = Encoding.GetEncoding("utf-8");
        public NSwriter(){
            Console.WriteLine("存在する名前空間は以下の通りです");
            string[] files = Directory.GetFiles(path, "*");
            foreach (string f in files){ //名前空間の取得と文字列置換
                using(StreamReader sr = new StreamReader(f,Encoding.GetEncoding("utf-8"))){
                    while (sr.Peek() != -1){    //読み取り対象の文字がなくなるまで処理
                        string line = sr.ReadLine();
                        if(line.StartsWith("namespace")){
                            line = line.Replace("{",";");
                            Console.WriteLine(" "+line);
                            line = line.Replace("namespace ","using ") + $" //{path}{f}";
                            name.Add(line);
                        }
                    }
                }
            }
            Console.Write("この名前空間を追記したいファイル名(ファイル名.形式)を入力してください.ファイルがなければ作成します"+Environment.NewLine+">>>");
            string file = Console.ReadLine();
            if(file == ""){return;}

            Console.Write("global宣言しますか : yes => 1 , no => 2"+Environment.NewLine+">>>");
            int global = int.Parse(Console.ReadLine());
            if(global != 1 && global != 2){return;}
            
            if(File.Exists(path+file) is false){
                var newfile = File.Create(path+file);
                newfile.Close();
            }
            foreach (string name in name){  //グローバル宣言を対象のファイルに記述            
                using(StreamWriter writer = new StreamWriter(path+file, true, enc)){
                    switch(global){
                        case 1: writer.WriteLine("global "+name); break;
                        case 2: writer.WriteLine(name) ; break;
                    }
                }
            }
        }
    }
}