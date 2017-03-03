using System;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AsyncronsMethods
{
    class Program
    {
        delegate string PointerToHugeTask();
        delegate double PointerToCalculateArea(int x, int y);
        static void MainAnomousMethod(string[] args)
        {
            PointerToHugeTask objPointer = HugeTask.DoHugeTask;
            //string msg = 
            //var d = delegate (IAsyncResult result) {
            //    PointerToHugeTask o = (PointerToHugeTask)result.AsyncState;
            //    string msg = o.EndInvoke(result);
            //};
            objPointer.BeginInvoke(new AsyncCallback(CallBackHere), objPointer);
            Thread.Sleep(1000);


            Console.WriteLine(objPointer.Invoke());
            Console.WriteLine("Waiting in main thread");

            PointerToCalculateArea carea = (r, x) => 3.14 * r * x;
            double area = carea.Invoke(2, 2);
            Console.WriteLine(area);

            Func<double, double, double> farea = (r, x) => 3.14 * r * x;
            Console.WriteLine(farea(10, 20));
            Console.ReadLine();

        }
        static void CallBackHere(IAsyncResult result)
        {
            PointerToHugeTask o = (PointerToHugeTask)result.AsyncState;
            string msg = o.EndInvoke(result);
            Console.WriteLine(msg+"Callback");
            //return msg;
        }

        static void Main(string[] args)
        {
            Employee emp = new Employee();
            emp.Name = "this is a duummy text";
            List<ValidationResult> vlst = new List<ValidationResult>();
            bool IsValid = Validator.TryValidateObject(emp, new ValidationContext(emp), vlst, true);
            //Validator.ValidateObject(emp, new ValidationContext(emp), true);
            //bool IsValid2 = Validator.TryValidateProperty(emp.Name, new ValidationContext(emp), vlst);
            //Console.WriteLine(IsValid);
            foreach (ValidationResult vr in vlst)
            {
                Console.WriteLine(vr.ErrorMessage);
                var mems = vr.MemberNames;
                foreach (var item in mems)
                {
                    Console.WriteLine(item);
                }
            }
            Console.Read();
        }
    }

    public class Employee
    {
        [Required(ErrorMessage = "check {0}")]
        //[MaxLength(5, ErrorMessage = "{0} {1}")]
        //[StringLength(20, MinimumLength = 2, ErrorMessage = "StringLength {0} {1}")]
        [Display(Name = "Employee Name")]
        [MaxWord(4, ErrorMessage = "{0} cannot exceed more than  word")]
        public string Name { get; set; }
    }

    public class MaxWordAttribute : ValidationAttribute
    {
        int _maxWord;
        public MaxWordAttribute() { _maxWord = 2; }
        public MaxWordAttribute(int m)
        {
            _maxWord = m;
        }
        public override bool IsValid(object value)
        {
            if (value.ToString().Split(' ').Length > _maxWord)
                return false;
            else
                return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().Split(' ').Length > _maxWord)
            {
                //validationContext.MemberName = "Dummy mummy";
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else
                return ValidationResult.Success;

        }
    }

    class HugeTask
    {
        public static string DoHugeTask()
        {
            Thread.Sleep(5000);
            return "Task Completed";
        }
    }
}
