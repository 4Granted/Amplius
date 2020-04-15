namespace Amplius.Data.UBJson
{
    /// <summary>
    /// An interface for serializable objects; provides a <c>Read</c> and <c>Write</c> method.
    /// </summary>
    public interface IUBSerializable
    {
        public UBObject Write(UBObject ub);
        public void Read(UBObject ub);
    }

    /// <summary>
    /// An abstract class for serializable objects; allows for the inherited class to be serialized as a <c>UBObject</c>.
    /// 
    /// <para>Although <c><see cref="UBSerializable"/></c> is more convienent, <c><see cref="IUBSerializable"/></c> is more customizable; as one is an interface.</para>
    /// </summary>
    public abstract class UBSerializable : UBObject
    {
        public abstract UBSerializable Write();
        public abstract void Read();
    }
}
