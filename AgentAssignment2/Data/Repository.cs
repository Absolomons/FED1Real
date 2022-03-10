using DebtBook.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace DebtBook.Data
{
    public class Repository
    {
        internal static ObservableCollection<debtor> ReadFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<debtor>));
            TextReader reader = new StreamReader(fileName);
            var debtors = (ObservableCollection<debtor>)serializer.Deserialize(reader);
            reader.Close();
            /*foreach (var deptor in debtors)
            {
                deptor.DebtList.RemoveAt(0);
            }
            */
            return debtors;
        }
        internal static void SaveFile(string fileName, ObservableCollection<debtor> agents)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<debtor>));
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, agents);
            writer.Close();
        }
    }
}
