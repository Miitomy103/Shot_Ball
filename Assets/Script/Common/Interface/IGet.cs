/// <summary>
/// 取得（入手）状態を持つオブジェクトが実装するインターフェース。コインなどに使用する。
/// </summary>
public interface IGet
{
    /// <summary>
    /// 取得済みかどうか。
    /// </summary>
    bool IsGet { get; }
}
