

public static class CommonFunc 
{
    /*public static List<T> Clone<T>(object List)
    {
        using (Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, List);
            objectStream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as List<T>;
        }
    }*/
    
    /*public static T Clone<T>(T RealObject) 
    {  
        using(Stream stream=new MemoryStream())
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, RealObject);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(stream);
        }
    }*/
    
    /*public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable   
    {   
        return listToClone.Select(item => (T)item.Clone()).ToList();   
    } */
}
