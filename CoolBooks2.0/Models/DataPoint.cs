using System.Runtime.Serialization;

namespace CoolBooks.Models
{
	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class DataPoint
	{
		//public DataPoint(List<string> label, int y)
		//{
		//	this.Label = label;
		//	this.Y = y;
		//}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label { get; set; }

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<int> Y {get;set;}

       // public DateTime ToDay { get; set; }

    }
}
