/**********************************************/
/*Project: CRF#                               */
/*Author: Zhongkai Fu                         */
/*Email: fuzhongkai@gmail.com                 */
/**********************************************/

using System;
using System.Collections.Generic;
using System.Text;
using CRFSharp;
using CRFSharpWrapper;
using System.IO;

namespace CRFSharpWrapper
{
    public class Decoder
    {
        ModelReader modelReader;

        //Load encoded model form file
        public bool LoadModel(string strModelFileName)
        {
            modelReader = new ModelReader();
            return modelReader.LoadModel(strModelFileName);
        }

        public SegDecoderTagger CreateTagger(int nbest, int this_crf_max_word_num = Utils.DEFAULT_CRF_MAX_WORD_NUM)
        {
            if (modelReader == null)
            {
                return null;
            }
            var tagger = new SegDecoderTagger(nbest, this_crf_max_word_num);
            tagger.init_by_model(modelReader);

            return tagger;
        }

        //Segment given text
        public int Segment(  crf_seg_out[] pout, //segment result
            SegDecoderTagger tagger, //Tagger per thread
            List<List<string>> inbuf //feature set for segment
            )
        {
            var ret = 0;
            if (inbuf.Count == 0)
            {
                //Empty input string
                return Utils.ERROR_SUCCESS;
            }

            ret = tagger.reset();
            if (ret < 0)
            {
                return ret;
            }

            ret = tagger.add(inbuf);
            if (ret < 0)
            {
                return ret;
            }

            //parse
            ret = tagger.parse();
            if (ret < 0)
            {
                return ret;
            }

            //wrap result
            ret = tagger.output(pout); 
            if (ret < 0)
            {
                return ret;
            }

            return Utils.ERROR_SUCCESS;
        }

        object rdLocker = new object();
        /// file nay se tong hop nhung record va luu thanh file test o ngoai o E
        public void TaoFileTestBIOES( )
        {
           
            List<int> danhsachfileTrain = new List<int>();
            if (Directory.Exists("G:\\Test"))
            {
             string[] filtemp =    Directory.GetFiles("G:\\Test");
             for (int i = 0; i < filtemp.Length; i++)
                 File.Delete(filtemp[i]);
            }
            Directory.CreateDirectory("G:\\Test");

            string[] recordList = (File.ReadAllText("G:\\k-fold\\log.txt")).Split('\t');
            for (int i = 0; i < recordList.Length; i++)
                if( recordList[i] !="" )
                danhsachfileTrain.Add(Convert.ToInt16(recordList[i]));
          string[] listRecord  = Directory.GetFiles("G:\\Features");
          
            foreach( string record in listRecord )
            {

                
               
                    if (!checkExits(danhsachfileTrain, System.IO.Path.GetFileNameWithoutExtension(record)))
                        try
                        {
                            File.Copy(record, "G:\\Test\\" + System.IO.Path.GetFileName(record));
                            
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                
                

            }
           
        }
        bool checkExits(List<int> danhsachfileTrain, string record)
        {
            for( int i =0; i< danhsachfileTrain.Count;i++ )
            {
                if (danhsachfileTrain[i].ToString() == record) return true;
            }
            return false;
        }
        /// goi qua goi lai met qua
       public bool TestCRF(string Path)
        {
      
         
         
            var options = new CRFSharpWrapper.DecoderArgs();
            options.maxword = 1000;
            options.nBest = 1;
            options.probLevel = 2;
            string PathD = Directory.GetParent(Path).ToString();
            options.strModelFileName = PathD+"\\model_file";
          
                options.strInputFileName = Path;

                options.strOutputFileName = PathD  + "\\Result" + System.IO.Path.GetFileNameWithoutExtension(Path).ToString();
                options.strOutputSegFileName = PathD + "\\OutSeg" ;
                options.thread = 13;

                var parallelOption = new System.Threading.Tasks.ParallelOptions();

                if (File.Exists(options.strInputFileName) == false)
                {
                    Console.WriteLine("FAILED: Open {0} file failed.", options.strInputFileName);
                    return false;
                }

                if (File.Exists(options.strModelFileName) == false)
                {
                    Console.WriteLine("FAILED: Open {0} file failed.", options.strModelFileName);
                    return false;
                }

                var sr = new StreamReader(options.strInputFileName);
                StreamWriter sw = null, swSeg = null;

                if (options.strOutputFileName != null && options.strOutputFileName.Length > 0)
                {
                    sw = new StreamWriter(options.strOutputFileName);
                }
                if (options.strOutputSegFileName != null && options.strOutputSegFileName.Length > 0)
                {
                    swSeg = new StreamWriter(options.strOutputSegFileName);
                }


                //Create CRFSharp wrapper instance. It's a global instance
                var crfWrapper = new CRFSharpWrapper.Decoder();
                //Load model from file
                if (crfWrapper.LoadModel(options.strModelFileName) == false)
                {
                    return false;
                }

                var queueRecords = new System.Collections.Concurrent.ConcurrentQueue<List<List<string>>>();
                var queueSegRecords = new System.Collections.Concurrent.ConcurrentQueue<List<List<string>>>();

                parallelOption.MaxDegreeOfParallelism = options.thread;
                System.Threading.Tasks.Parallel.For(0, options.thread, parallelOption, t =>
                {

                    //Create decoder tagger instance. If the running environment is multi-threads, each thread needs a separated instance
                    var tagger = crfWrapper.CreateTagger(options.nBest, options.maxword);
                    tagger.set_vlevel(options.probLevel);

                    //Initialize result
                    var crf_out = new crf_seg_out[options.nBest];
                    for (var i = 0; i < options.nBest; i++)
                    {
                        crf_out[i] = new crf_seg_out(tagger.crf_max_word_num);
                    }

                    var inbuf = new List<List<string>>();
                    while (true)
                    {
                        lock (rdLocker)
                        {
                            if (ReadRecord(inbuf, sr) == false)
                            {
                                break;
                            }

                            queueRecords.Enqueue(inbuf);
                            queueSegRecords.Enqueue(inbuf);
                        }

                        //Call CRFSharp wrapper to predict given string's tags
                        if (swSeg != null)
                        {
                            crfWrapper.Segment(crf_out, tagger, inbuf);
                        }
                        else
                        {
                            crfWrapper.Segment((CRFSharp.crf_term_out[])crf_out, (CRFSharp.DecoderTagger)tagger, inbuf);
                        }

                        List<List<string>> peek = null;
                        //Save segmented tagged result into file
                        if (swSeg != null)
                        {
                            var rstList = ConvertCRFTermOutToStringList(inbuf, crf_out);
                            while (peek != inbuf)
                            {
                                queueSegRecords.TryPeek(out peek);
                            }
                            for (int index = 0; index < rstList.Count; index++)
                            {
                                var item = rstList[index];
                                swSeg.WriteLine(item);
                            }
                            queueSegRecords.TryDequeue(out peek);
                            peek = null;
                        }

                        //Save raw tagged result (with probability) into file
                        if (sw != null)
                        {
                            while (peek != inbuf)
                            {
                                queueRecords.TryPeek(out peek);
                            }
                            OutputRawResultToFile(inbuf, crf_out, tagger, sw);
                            queueRecords.TryDequeue(out peek);
                       //     Console.WriteLine("Het 1 cau");

                        }
                    }
                });


                sr.Close();

                if (sw != null)
                {
                    sw.Close();
                }
                if (swSeg != null)
                {
                    swSeg.Close();
                }
         
                return true;
            
        }
       public bool TestCRFKFold(string Path)
       {


           var options = new CRFSharpWrapper.DecoderArgs();
           options.maxword = 1000;
           options.nBest = 1;
           options.probLevel = 2;
           string file = Path + "\\test.txt";
            //string[] suatfile = File.ReadAllLines(file);
            //StreamWriter stw = File.CreateText(Path + "\\test1.txt");
            //stw.WriteLine(suatfile[0]);
            //stw.Flush();
            //for ( int i = 1; i < suatfile.Length; i++)
            //{
            //    if (suatfile[i] == "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"
            //        && suatfile[i - 1] == "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t")
            //        continue;
            //    else
            //    {
            //        stw.WriteLine(suatfile[i]);
            //        stw.Flush();
            //    }
                    


            //}
            //stw.Close();
           options.strModelFileName = Path + "\\model_file";
          
               options.strInputFileName = file;

               options.strOutputFileName = System.IO.Path.GetDirectoryName(file) + "\\Result" + System.IO.Path.GetFileName(file);
            options.strOutputSegFileName =  System.IO.Path.GetDirectoryName(file) + "\\OutSeg" + System.IO.Path.GetFileName(file);
               options.thread = 3;

               var parallelOption = new System.Threading.Tasks.ParallelOptions();

               if (File.Exists(options.strInputFileName) == false)
               {
                   Console.WriteLine("FAILED: Open {0} file failed.", options.strInputFileName);
                   return false;
               }

               if (File.Exists(options.strModelFileName) == false)
               {
                   Console.WriteLine("FAILED: Open {0} file failed.", options.strModelFileName);
                   return false;
               }

               var sr = new StreamReader(options.strInputFileName);
               StreamWriter sw = null, swSeg = null;

               if (options.strOutputFileName != null && options.strOutputFileName.Length > 0)
               {
                   sw = new StreamWriter(options.strOutputFileName);
               }
               if (options.strOutputSegFileName != null && options.strOutputSegFileName.Length > 0)
               {
                   swSeg = new StreamWriter(options.strOutputSegFileName);
               }


               //Create CRFSharp wrapper instance. It's a global instance
               var crfWrapper = new CRFSharpWrapper.Decoder();
               //Load model from file
               if (crfWrapper.LoadModel(options.strModelFileName) == false)
               {
                   return false;
               }

               var queueRecords = new System.Collections.Concurrent.ConcurrentQueue<List<List<string>>>();
               var queueSegRecords = new System.Collections.Concurrent.ConcurrentQueue<List<List<string>>>();

               parallelOption.MaxDegreeOfParallelism = options.thread;
               System.Threading.Tasks.Parallel.For(0, options.thread, parallelOption, t =>
               {

                   //Create decoder tagger instance. If the running environment is multi-threads, each thread needs a separated instance
                   var tagger = crfWrapper.CreateTagger(options.nBest, options.maxword);
                   tagger.set_vlevel(options.probLevel);

                   //Initialize result
                   var crf_out = new crf_seg_out[options.nBest];
                   for (var i = 0; i < options.nBest; i++)
                   {
                       crf_out[i] = new crf_seg_out(tagger.crf_max_word_num);
                   }

                   var inbuf = new List<List<string>>();
                   while (true)
                   {
                       lock (rdLocker)
                       {
                           if (ReadRecord(inbuf, sr) == false)
                           {
                               break;
                           }

                           queueRecords.Enqueue(inbuf);
                           queueSegRecords.Enqueue(inbuf);
                       }

                       //Call CRFSharp wrapper to predict given string's tags
                       if (swSeg != null)
                       {
                           crfWrapper.Segment(crf_out, tagger, inbuf);
                       }
                       else
                       {
                           crfWrapper.Segment((CRFSharp.crf_term_out[])crf_out, (CRFSharp.DecoderTagger)tagger, inbuf);
                       }

                       List<List<string>> peek = null;
                       //Save segmented tagged result into file
                       if (swSeg != null)
                       {
                           var rstList = ConvertCRFTermOutToStringList(inbuf, crf_out);
                           while (peek != inbuf)
                           {
                               queueSegRecords.TryPeek(out peek);
                           }
                           for (int index = 0; index < rstList.Count; index++)
                           {
                               var item = rstList[index];
                               swSeg.WriteLine(item);
                           }
                           queueSegRecords.TryDequeue(out peek);
                           peek = null;
                       }

                       //Save raw tagged result (with probability) into file
                       if (sw != null)
                       {
                           while (peek != inbuf)
                           {
                               queueRecords.TryPeek(out peek);
                           }
                           OutputRawResultToFile(inbuf, crf_out, tagger, sw);
                           queueRecords.TryDequeue(out peek);
                        //   Console.WriteLine("Het 1 cau");

                       }
                   }
               });


               sr.Close();

               if (sw != null)
               {
                   sw.Close();
               }
               if (swSeg != null)
               {
                   swSeg.Close();
               }
            Console.WriteLine("Test CRF DONE");
           return true;

       }
        private void OutputRawResultToFile(List<List<string>> inbuf, CRFSharp.crf_term_out[] crf_out, SegDecoderTagger tagger, StreamWriter sw)
        {
            for (var k = 0; k < crf_out.Length; k++)
            {
                if (crf_out[k] == null)
                {
                    //No more result
                    break;
                }

                var sb = new StringBuilder();

                var crf_seg_out = crf_out[k];
                //Show the entire sequence probability
                //For each token
                for (var i = 0; i < inbuf.Count; i++)
                {
                    //Show all features
                    for (var j = 0; j < inbuf[i].Count; j++)
                    {
                        sb.Append(inbuf[i][j]);
                        sb.Append("\t");
                    }

                    //Show the best result and its probability
                    sb.Append(crf_seg_out.result_[i]);

                    if (tagger.vlevel_ > 1)
                    {
                        sb.Append("\t");
                        sb.Append(crf_seg_out.weight_[i]);

                        //Show the probability of all tags
                        sb.Append("\t");
                        for (var j = 0; j < tagger.ysize_; j++)
                        {
                            sb.Append(tagger.yname(j));
                            sb.Append("/");
                            sb.Append(tagger.prob(i, j));

                            if (j < tagger.ysize_ - 1)
                            {
                                sb.Append("\t");
                            }
                        }
                    }
                    sb.AppendLine();

                }
                if (tagger.vlevel_ > 0)
                {
                    sw.WriteLine("#{0}", crf_seg_out.prob);
                }
                sw.WriteLine(sb.ToString().Trim());
                sw.WriteLine();
               
            }
        }
        private List<string> ConvertCRFTermOutToStringList(List<List<string>> inbuf, crf_seg_out[] crf_out)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < inbuf.Count; i++)
            {
                sb.Append(inbuf[i][0]);
            }

            var strText = sb.ToString();
            var rstList = new List<string>();
            for (var i = 0; i < crf_out.Length; i++)
            {
                if (crf_out[i] == null)
                {
                    //No more result
                    break;
                }

                sb.Clear();
                var crf_term_out = crf_out[i];
                for (var j = 0; j < crf_term_out.Count; j++)
                {
                    var str = strText.Substring(crf_term_out.tokenList[j].offset, crf_term_out.tokenList[j].length);
                    var strNE = crf_term_out.tokenList[j].strTag;

                    sb.Append(str);
                    if (strNE.Length > 0)
                    {
                        sb.Append("[" + strNE + "]");
                    }
                    sb.Append(" ");
                }
                rstList.Add(sb.ToString().Trim());
            }

            return rstList;
        }

        private bool ReadRecord(List<List<string>> inbuf, StreamReader sr)
        {
            inbuf.Clear();

            while (true)
            {
                var strLine = sr.ReadLine();
                if (strLine == null)
                {
                    //At the end of current file
                    if (inbuf.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                strLine = strLine.Trim();
                if (strLine.Length == 0)
                {
                    return true;
                }

                //Read feature set for each record
                var items = strLine.Split(new char[] { '\t' });
                inbuf.Add(new List<string>());
                for (int index = 0; index < items.Length; index++)
                {
                    var item = items[index];
                    inbuf[inbuf.Count - 1].Add(item);
                }
            }
        }

        //Segment given text
        public int Segment(crf_term_out[] pout, //segment result
            DecoderTagger tagger, //Tagger per thread
            List<List<string>> inbuf //feature set for segment
            )
        {
            var ret = 0;
            if (inbuf.Count == 0)
            {
                //Empty input string
                return Utils.ERROR_SUCCESS;
            }

            ret = tagger.reset();
            if (ret < 0)
            {
                return ret;
            }

            ret = tagger.add(inbuf);
            if (ret < 0)
            {
                return ret;
            }

            //parse
            ret = tagger.parse();
            if (ret < 0)
            {
                return ret;
            }

            //wrap result
            ret = tagger.output(pout);
            if (ret < 0)
            {
                return ret;
            }

            return Utils.ERROR_SUCCESS;
        }



        
    }
}
