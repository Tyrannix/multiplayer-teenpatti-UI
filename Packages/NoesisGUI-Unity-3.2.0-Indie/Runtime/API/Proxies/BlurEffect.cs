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

public class BlurEffect : Effect {
  internal new static BlurEffect CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new BlurEffect(cPtr, cMemoryOwn);
  }

  internal BlurEffect(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(BlurEffect obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public BlurEffect() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_BlurEffect();
  }

  public static DependencyProperty RadiusProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.BlurEffect_RadiusProperty_get();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public float Radius {
    set {
      NoesisGUI_PINVOKE.BlurEffect_Radius_set(swigCPtr, value);
    } 
    get {
      float ret = NoesisGUI_PINVOKE.BlurEffect_Radius_get(swigCPtr);
      return ret;
    } 
  }

}

}

