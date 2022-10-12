using System;
using System.Collections.Generic;
using System.Numerics;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibrary
{

    public enum ChangeInfo { ItemChanged, Add, Remove, Replace };
    public class DataChangedEventArgs
    {
        public ChangeInfo info { set; get; }
        public int num { set; get; }

        public DataChangedEventArgs(ChangeInfo i, int n)
        {
            info = i;
            num = n;
        }

        public override string ToString()
        {
            return $"Data changed due to: {info}\nAmount of elements: {num}";
        }
    }
    public delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);

    [Serializable]
    public class V4MainCollection : IEnumerable<V4Data>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event DataChangedEventHandler DataChanged;
        [field:NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        List<V4Data> list { get; set; }

        public V4MainCollection()
        {
            list = new List<V4Data>();
            ChangesWereMade = true;
            CollectionChanged += V4MainCollection_CollectionChanged;
        }

        public bool ChangesWereMade { get; set; }

        public void Save(string filename)
        {
            FileStream fileStream = null;
             try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, this);
                ChangesWereMade = false;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Save\n"+ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public static V4MainCollection Load(string filename)
        {
            FileStream fileStream = null;
            V4MainCollection res = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                res = binaryFormatter.Deserialize(fileStream) as V4MainCollection;
                res.CollectionChanged += res.V4MainCollection_CollectionChanged;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Load\n" + ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return res;
        }

        

        private void V4MainCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            V4MainCollection sender_ = (V4MainCollection)sender;
            sender_.ChangesWereMade = true;

            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("max_complex"));
        }

        void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            OnDataChanged(ChangeInfo.ItemChanged, list.Count);
        }

        protected void OnDataChanged(ChangeInfo info, int n)
        {
            if (DataChanged != null)
                DataChanged(this, new DataChangedEventArgs(info, n));
        }

        public V4Data this[int index]
        {
            get => list[index];
            set
            {
                //list[index].PropertyChanged -= HandlePropertyChanged;
                list[index] = value;
                //list[index].PropertyChanged += HandlePropertyChanged;
                //OnDataChanged(ChangeInfo.Replace, list.Count);
                
                if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); 
            }
        }

        public IEnumerator<V4Data> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        
        public int Count
        {
            get { return list.Count; }
        }

        public void Add(V4Data item)
        {
            list.Add(item);
            //list[Count-1].PropertyChanged += HandlePropertyChanged;
            //item.PropertyChanged += HandlePropertyChanged;
            //OnDataChanged(ChangeInfo.Add, list.Count);

            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Remove(string id, double w)
        {
            bool res = false;
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Info == id) && (list[i].Frequency == w))
                {
                    //list[i].PropertyChanged -= HandlePropertyChanged;
                    list.RemoveAt(i);
                    //OnDataChanged(ChangeInfo.Remove, list.Count);
                    i--;
                    res = true;

                    if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
            return res;
        }

        public bool Remove2(V4Data item)
        {
            bool res = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == item)
                {
                    //list[i].PropertyChanged -= HandlePropertyChanged;
                    list.RemoveAt(i);
                    //OnDataChanged(ChangeInfo.Remove, list.Count);
                    i--;
                    res = true;

                    if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
            return res;
        }

        public void AddDefaults()
        {
            Grid2D temp = new Grid2D(0.5f, 0.7f, 1, 2);
            V4DataOnGrid first = new V4DataOnGrid("123", 111d, temp);
            first.InitRandom(4.5, 6.1);

            temp = new Grid2D(0.8f, 1.2f, 0, 0);
            V4DataOnGrid second = new V4DataOnGrid("123", 111d, temp);
            second.InitRandom(0.1, 4.3);

            temp = new Grid2D(3.2f, 3.1f, 5, 1);
            V4DataOnGrid third = new V4DataOnGrid("789", 333.45d, temp);
            third.InitRandom(11.4, 15.2);

            V4DataCollection forth = new V4DataCollection("123", 111d);
            forth.InitRandom(6, 1.4f, 2.6f, 4.5, 6.1);

            V4DataCollection fifth = new V4DataCollection("234", 567d);
            fifth.InitRandom(6, 0.1f, 0.6f, 8.5, 9.1);

            V4DataCollection sixth = new V4DataCollection("345", 678d);
            sixth.InitRandom(0, 2.4f, 1.6f, 2.5, 3.1);

            Add(first);
            Add(second);
            Add(third);
            Add(forth);
            Add(fifth);
            Add(sixth);
            if (CollectionChanged != null) CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public override string ToString()
        {
            return string.Join("\n", list);
            //Сцепляет строковые представления объектов из массива,
            //помещая между ними заданный разделитель.
        }

        public string ToLongString(string format)
        {
            string res = "";
            for (int i = 0; i < list.Count; i++)
            {
                res = res + list[i].ToLongString(format) + "\n";
            }
            return res;
        }

        public DataItem maxAbs
        {
            get
            {
                return list.SelectMany(x => x).OrderByDescending(v => v.compl.Magnitude).First();
            }
        }
        
        public IEnumerable<DataItem> desc
        {
            get
            {
                return list.SelectMany(x => x).Distinct().OrderByDescending(v => v.compl.Magnitude);

            }
        }

        public float maxDistance
        {
            get
            {   
                var query = list.SelectMany(x => x).ToList();
                var query1 = from item1 in query
                             from item2 in query
                             select Vector2.Distance(item1.vect, item2.vect);
                float max = query1.OrderByDescending(x => x).First();
                return max;
            }
        }

        public Complex max_complex
        {
            get
            {
                return list.SelectMany(x => x).OrderByDescending(v => v.compl.Magnitude).FirstOrDefault().compl;
            }
        }

        public bool Contains(string id)
        {
            foreach (V4Data item in list)
                if (item.Info == id) return true;
            return false;
        }
    }
}
