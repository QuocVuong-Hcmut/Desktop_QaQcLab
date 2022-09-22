using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_cha_qaqc_phase2.Core.Domain.Models
{
    public class ErrorCode
    {
        public IDictionary<int, string> EnduranceWarningCode = new Dictionary<int, string>() {
            {100, "Đã hoàn thành quy trình kiểm tra" },
            {500, "Cài đặt lực ,thời gian giữ , số lần nhấn và sai số" },
            {501, "Lực cài đặt hệ 1 quá lớn " },
            {502, "Lực cài đặt hệ 2 quá lớn " },
            {503, "Hệ thống chưa sẵn sàng" },
            {504, "Lỗi xi lanh 1 chưa tới vị trí đặt lực" },
            {505, "Lỗi xi lanh 1 chưa về vị trí ban đầu" },
            {506, "Lỗi xi lanh 2 chưa tới vị trí đặt lực" },
            {507, "Lỗi xi lanh 2 chưa về vị trí ban đầu" },
            {508, "Lỗi xi lanh 3 chưa tới vị trí đặt lực" },
            {509, "Lỗi xi lanh 3 chưa về vị trí ban đầu " },
            {510, "Dừng khẩn cấp" },
            {600, "Xi lanh 1 quá lực" },
            {601, "Xi lanh 2 quá lực" },
            {602, "Xi lanh 3 quá lực" },
            {603, "Xi lanh 1 thiếu lực" },
            {604, "Xi lanh 2 thiếu lực" },
            {605, "Xi lanh 3 thiếu lực" },
            {606, "Relay 1 đạt giới hạn" },
            {607, "Relay 2 đạt giới hạn" },
            {608, "Relay 3 đạt giới hạn" },
            {609, "Relay 4 đạt giới hạn" },
            {610, "Relay 5 đạt giới hạn" },
            {611, "Relay 6 đạt giới hạn" }
        };
        public IDictionary<int, string> WaterProofingWarningCode = new Dictionary<int, string>() {
            { 100, "Đã hoàn thành quy trình kiểm tra (100)"},
            { 500, "Nhiệt độ vượt giới hạn công tắc nhiệt (500)"},
            { 501, "Nhiệt độ vượt ngưỡng cho phép (501)" },
            { 600, "Mức nước không đủ (600)" },
        };
        public IDictionary<int, string> SoftCloseWarningCode = new Dictionary<int, string>()
        {
             {101, "Lỗi xi lanh 1 không đẩy ra (101)" },
             {102, "Lỗi xi lanh 2 không đẩy ra (102)" },
             {103, "Đã hoàn thành quy trình kiểm tra (103)" },
             {104, "Cần thay thế Relay (104)" },
             {105,"Cần nhấn Stop trước khi chuyển sang chế độ Bằng tay (105)" },
             {106, "Lỗi cảm biến gắn trên nắp bàn cầu (106)" }
        };

        public IDictionary<int, string> ForcedCloseWarningCode = new Dictionary<int, string>()
        {
         {201, "Lỗi xi lanh không đẩy ra (201)" },
         {202, "Lỗi xi lanh không lui về (202)" },
         {203, "Đã hoàn thành quy trình kiểm tra (203)" },
         {204, "Cần thay thế Relay (204)" },
         {205,"Cần nhấn Stop trước khi chuyển sang chế độ Bằng tay (205)" },
        };
    }
    public class ListEvent
    {
        public DateTime? Time { get; set; }
        public string Event { get; set; }
        public ListEvent(DateTime? time, string Event)
        {
            this.Time = time;
            this.Event = Event;
        }

    }
}
