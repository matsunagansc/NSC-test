namespace Files{
    public class FilePath{  //
        protected static string cp = ""; //current path 階層パス
        public FilePath(){
            cp = Directory.GetCurrentDirectory();   //現在の階層を取得（表記は一つ上のフォルダまで）
            Console.WriteLine("パスを設定しました.");
        }
        public void FileFind(){
            string[] files = Directory.GetFiles(cp, "*");   //指定フォルダ内にあるファイル名を取得.第3引数(SearchOption.AllDirectories)にて階下の全ファイルも検索.
            foreach(string str in files){
                Console.WriteLine(str.Replace(cp,""));
            }
        }
    }
    public class FileWrite : FilePath{
        public FileWrite(){
            Console.WriteLine("入力モード開始");
            string? str = "";
            do{    //空文字入力まで継続
                Console.Write("(write)>>> ");
                str = Console.ReadLine();
                writedo(str);
            }while(str != "");
        }
        public void writedo(string? str){
            Encoding enc = Encoding.GetEncoding("utf-8");
            using(StreamWriter writer = new StreamWriter(cp+"test.txt", false, enc)){  //引数=>対象ファイル,追記:true/上書き:false,エンコード指定
                writer.WriteLine(str);
            }   //usingステートメントを使っているのでwriter.Closeしない（括弧を抜けた時点で破棄される）
        }
    }
    public class FileOut : FilePath{
        private static string path = ファイルの出力先パス;
        public FileOut(){
            Console.WriteLine("指定したファイルを出力します 出力先:"+path);
            FileFind();
            string? str = "";
            List<string> files = new();
            do{
                Console.Write("(output)>>> ");
                str = Console.ReadLine();
                if(File.Exists(cp+str)){files.Add(str);}
                else if(str != ""){
                    Console.WriteLine(cp+"内に "+str+" というファイルが存在しません.");
                }
            }while(str != "");
            foreach(string f in files){
                outdo(f);
            }
        }
        public void outdo(string str){
            File.Copy(cp+str,path+str);
            Console.WriteLine(str+" を出力しました.");
        }
    }
}