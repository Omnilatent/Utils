
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

**CommonFunctions**: Tổng hợp nhiều hàm hữu ích thường dùng.

## Tests

Trong thư viện có sẵn một số cái test để kiểm tra dự án có cài những thư viện cần thiết chưa.

Nếu package này được cài bằng Unity Package Manager, bạn cần bấm vào menu `Tools/Omnilatent/Enable Utility Tests` để Unity biết là package này có file Tests. 