using System.Collections.Generic;

public struct SocketResponse<T>{
    public bool success;
    public T data;
}