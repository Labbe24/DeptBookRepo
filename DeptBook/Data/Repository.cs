using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DeptBook.Models;

namespace DeptBook.Data
{
    public class Repository
    {
        internal static void ReadFile(string fileName, out ObservableCollection<Debtor> debtors)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
            TextReader reader = new StreamReader(fileName);
            // Deserialize all the agents.
            debtors = (ObservableCollection<Debtor>)serializer.Deserialize(reader);
            reader.Close();
        }

        internal static void SaveFile(string fileName, ObservableCollection<Debtor> debtors)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Debtor>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize all the agents.
            serializer.Serialize(writer, debtors);
            writer.Close();
        }
    }
}
