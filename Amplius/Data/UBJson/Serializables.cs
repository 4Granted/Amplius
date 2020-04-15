/// <license>
/// MIT License
/// 
/// Copyright(c) 2020 RuthlessBoi
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </license>

namespace Amplius.Data.UBJson
{
    /// <summary>
    /// An interface for serializable objects; provides a <c>Read</c> and <c>Write</c> method.
    /// </summary>
    public interface IUBSerializable
    {
        public UBObject Serialize(UBObject ub);
        public void Deserialize(UBObject ub);
    }

    /// <summary>
    /// An abstract class for serializable objects; allows for the inherited class to be serialized as a <c>UBObject</c>.
    /// 
    /// <para>Although <c><see cref="UBSerializable"/></c> is more convienent, <c><see cref="IUBSerializable"/></c> is more customizable; as one is an interface.</para>
    /// </summary>
    public abstract class UBSerializable : UBObject
    {
        public abstract UBSerializable Serialize();
        public abstract void Deserialize();
    }
}
