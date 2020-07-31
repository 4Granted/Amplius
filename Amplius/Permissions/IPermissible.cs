using System.Collections.Immutable;

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

namespace Amplius.Permissions
{
    /// <summary>
    /// Represents an object that holds <see cref="Permission"/> data
    /// </summary>
    public interface IPermissible
    {
        /// <summary>
        /// Adds a <see cref="Permission"/> to the <see cref="IPermissible"/>
        /// </summary>
        /// <param name="permission"><see cref="Permission"/> to add</param>
        public void AddPermission(Permission permission);
        /// <summary>
        /// Removes a <see cref="Permission"/> to the <see cref="IPermissible"/>
        /// </summary>
        /// <param name="permission"><see cref="Permission"/> to remove</param>
        public void RemovePermission(Permission permission);

        /// <summary>
        /// Gets all <see cref="Permission"/>'s held by the <see cref="IPermissible"/>
        /// </summary>
        /// <returns>Returns all held <see cref="Permission"/>'s</returns>
        public ImmutableArray<Permission> GetPermissions();
    }
}
