using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Differences
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running tests");
            Console.ReadKey();

            // First Initial Test

            var myFirstOb = new SomeType();
            myFirstOb.Forename = "David";
            myFirstOb.DateOfBirth = new DateTime(1990, 5, 2);
            myFirstOb.SpecialNumber = 23;

            var mySecondOb = new SomeType();
            mySecondOb.Forename = "Janet";
            mySecondOb.DateOfBirth = new DateTime(2000, 11, 22);
            mySecondOb.SpecialNumber = 23;

            extensions.DetailedCompare(myFirstOb, mySecondOb);
            extensions.ListDifferences();

            // Second Test

            var myThirdOb = new SomeType();
            myThirdOb.Forename = "Ian";
            myThirdOb.DateOfBirth = new DateTime(1981, 8, 12);
            myThirdOb.SpecialNumber = 11;

            var myFourthOb = new SomeType();
            myFourthOb.Forename = "Joanne";
            myFourthOb.DateOfBirth = new DateTime(1982, 2, 9);
            myFourthOb.SpecialNumber = 5;

            extensions.DetailedCompare(myThirdOb, myFourthOb);
            extensions.ListDifferences();

            // Third Test

            var myFifthOb = new SomeType();
            myFifthOb.Forename = "Trent";
            myFifthOb.DateOfBirth = new DateTime(1992, 6, 29);
            myFifthOb.SpecialNumber = 21;

            var mySixthOb = new SomeType();
            mySixthOb.Forename = "Andy";
            mySixthOb.DateOfBirth = new DateTime(1992, 6, 29);
            mySixthOb.SpecialNumber = 21;

            extensions.DetailedCompare(myFifthOb, mySixthOb);
            extensions.ListDifferences();

            // Fourth Test

            var mySeventhOb = new SomeType();
            mySeventhOb.Forename = "Bill";
            mySeventhOb.DateOfBirth = new DateTime(1989, 12, 3);
            mySeventhOb.SpecialNumber = 8;

            var myEigthhOb = new SomeType();
            myEigthhOb.Forename = "Claire";
            myEigthhOb.DateOfBirth = new DateTime(1992, 8, 13);
            myEigthhOb.SpecialNumber = 9;

            extensions.DetailedCompare(mySeventhOb, myEigthhOb);
            extensions.ListDifferences();

            // Fifth Test

            var myNinthOb = new SomeType();
            myNinthOb.Forename = "Lisa";
            myNinthOb.DateOfBirth = new DateTime(1985, 7, 13);
            myNinthOb.SpecialNumber = 15;

            var myTenthOb = new SomeType();
            myTenthOb.Forename = "Lisa";
            myTenthOb.DateOfBirth = new DateTime(1985, 7, 13);
            myTenthOb.SpecialNumber = 15;

            extensions.DetailedCompare(myNinthOb, myTenthOb);
            extensions.ListDifferences();
        }
    }

    // Add User Defined Type to work with any type
    public class UserDefinedType<T>
    {
        public T Property { get; set; }
    }

    // Create class of SomeType containing Forename, DateOfBirth, and SpecialNumber
    public class SomeType : IEquatable<SomeType>
    {
        // Can add name of SomeType object to distinguish objects
        //public string Name { get; set; }
        public string Forename { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SpecialNumber { get; set; }

        #region Implementation of equality

        public bool Equals(SomeType other) {
	
	        if (other == null)
		    {
		        return false;
	        }
		
            if (object.ReferenceEquals(other, null))
            {		
		         return false;
	        }

            if (this.Forename == other.Forename & this.DateOfBirth == other.DateOfBirth & this.SpecialNumber == other.SpecialNumber)
            {
                return true;
            }
            else
            {
                return false;
            }

	        //if( EqualityComparer<string>.Default.Compare(Name, other.Name) == false)
            //{
            //    return false;
            //}
		
        // Comparison logic here.
        // return X.Equals(other.X) && Y.Equals(other.Y);
        //throw new Exception("The method or operation is not implemented.");

            //return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SomeType);
        }

        public static bool operator ==(SomeType lhs, SomeType rhs)
        {
            return
                object.ReferenceEquals(lhs, rhs) ||
                !object.ReferenceEquals(lhs, null) && lhs.Equals(rhs);
        }

        public static bool operator !=(SomeType lhs, SomeType rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode() {
        // TODO Implement comparison logic here. E.g.:
        //return X.GetHashCode() ^ Y.GetHashCode();
            return this.GetHashCode();
        //throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }


    static class extensions
    {
        public static List<Difference> variances = null;
        public static List<Difference> DetailedCompare<T>(this T val1, T val2)
        {
            Console.WriteLine("Comparing objects: " + val1.ToString() + " and " + val2.ToString());
            variances = new List<Difference>();
            FieldInfo[] fi = val1.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            //.ToList();
            //.ForEach(f => Console.WriteLine(f.Name));
            //FieldInfo[] fi = val1.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {
                Difference v = new Difference();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);
                if (!v.valA.Equals(v.valB))
                    variances.Add(v);

            }
            return variances;
        }

        public static string ListDifferences()
        {
            string listDifferences = "";
            if (variances.Count != 0)
            {
                foreach (Difference diff in variances)
                {
                    // Write differences in properties between two objects here     
                    listDifferences = diff.Prop.ToString() + " changed from " + diff.valA.ToString() + " to " + diff.valB.ToString() + System.Environment.NewLine;
                    Console.Write(listDifferences);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Write("No differences!");
                Console.ReadKey();
            }
            return listDifferences;
        }
    }
    class Difference
    {
        string _prop;
        public string Prop
        {
            get { return _prop; }
            set { _prop = value; }
        }
        object _valA;
        public object valA
        {
            get { return _valA; }
            set { _valA = value; }
        }
        object _valB;
        public object valB
        {
            get { return _valB; }
            set { _valB = value; }
        }

    }
}
