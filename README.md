
## USAGE:

Vietnamese:

**DebugManager**: kiểm soát DebugMode
- DebugModeActive: chế độ DebugMode có đang được kích hoạt không.

**DebugSwitchActive**: Bật/tắt gameObject dựa theo chế độ:
- ShowInDebugBuild: hiện khi bản build là Developement Build.
- ShowOnlyInDebugMode: chỉ hiện khi DebugManager.DebugModeActive = true và bản build là Developement Build.

**ErrorLogger**: Tự động log mọi Error và Exception ra 1 file text trong device.
- Gọi hàm ErrorLogger.Initialize() để kích hoạt chức năng tự động log lỗi.

**FPSCounter**: Hiển thị FPS lên TextMeshProUGUI.

**ToastMessage**: 
- ShowMessage(): Hiện một cái Toast lên, tự động biến mất sau 5 giây (có thể thay đổi bằng param).
