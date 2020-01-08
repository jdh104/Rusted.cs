
namespace Rusted
{
    [System.Obsolete("This is a compile-time check to prevent you from accidentally use the wrong version of a method")]
    public class UnusableObject
    {
        public UnusableObject()
        {
            throw new System.Exception("You tried to use the UnusableObject class");
        }
    }

    public class WrongMethodException : System.Exception { }
}
