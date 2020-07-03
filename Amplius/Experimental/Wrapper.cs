namespace Amplius.Experimental
{
    /// <summary>
    /// Proposed <see cref="Wrapper{T}"/> struct; used to contain data of type <typeparamref name="T"/>.
    /// 
    /// <para>A genericized struct for wrapping values in objects.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Experimental]
    public struct Wrapper<T>
    {
        public T Data => data;

        private readonly T data;

        public Wrapper(T data) => this.data = data;

        public static implicit operator T(Wrapper<T> wrapper) => wrapper.Data;
        public static explicit operator Wrapper<T>(T data) => new Wrapper<T>(data);
    }
}
