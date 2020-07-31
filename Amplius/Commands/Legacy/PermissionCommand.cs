using Amplius.Commands.Legacy;
using Amplius.Permissions;

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

namespace Amplius.Tests
{
    public abstract class PermissionCommand : CommandRepresentation, IPermissionValidator
    {
        protected readonly Permission permission;

        public PermissionCommand(string name, Permission? permission = null) : base(name) => this.permission = permission;

        public override abstract bool Execute(Command? command);

        public bool IsPermitted(IPermissible holder)
        {
            var holderPerms = holder.GetPermissions();

            if (permission == null)
                return true;

            foreach (var perm in holderPerms)
            {
                if (permission?.Key == perm?.Key)
                    return true;
            }

            return false;
        }
    }
}
