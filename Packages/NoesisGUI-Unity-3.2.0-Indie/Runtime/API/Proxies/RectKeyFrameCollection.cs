//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class RectKeyFrameCollection : FreezableCollection<RectKeyFrame> {
  internal new static RectKeyFrameCollection CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new RectKeyFrameCollection(cPtr, cMemoryOwn);
  }

  internal RectKeyFrameCollection(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(RectKeyFrameCollection obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public RectKeyFrameCollection() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_RectKeyFrameCollection();
  }

}

}

