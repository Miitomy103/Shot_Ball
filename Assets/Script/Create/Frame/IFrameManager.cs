namespace ShotBall.Create
{
    /// <summary>
    /// フレームの管理者が実装するインターフェース。
    /// </summary>
    public interface IFrameManager
    {
        /// <summary>
        /// 指定したcreateObjectをフレームに格納する。
        /// </summary>
        void InObject(CreateObject createObject);
    }
}