import os
from moviepy.editor import VideoFileClip
from concurrent.futures import ThreadPoolExecutor, as_completed

# 获取当前脚本所在的文件夹路径
current_folder = os.path.dirname(os.path.abspath(__file__))

# 设置视频文件夹和输出 GIF 文件夹
input_folder = os.path.join(current_folder, "videos")  # 输入文件夹名
output_folder = os.path.join(current_folder, "gifs")  # 输出文件夹名

# 创建输出文件夹（如果不存在）
os.makedirs(output_folder, exist_ok=True)


def convert_video_to_gif(filename):
    video_path = os.path.join(input_folder, filename)
    gif_filename = os.path.splitext(filename)[0] + ".gif"
    gif_path = os.path.join(output_folder, gif_filename)

    try:
        # 检查输入文件是否存在
        if not os.path.exists(video_path):
            return f"Error: Input file {filename} does not exist"

        # 检查输出文件是否已存在
        if os.path.exists(gif_path):
            return f"Skip: {gif_filename} already exists"

        # 加载视频并转换为 GIF
        with VideoFileClip(video_path) as clip:
            # 调整分辨率，设定较小的宽度（高度会自动按比例缩放）
            target_width = 640  # 你可以根据需要调整宽度
            clip_resized = clip.resize(width=target_width)

            # 可以添加一些优化参数，比如调整分辨率和帧率
            clip_resized.write_gif(
                gif_path,
                fps=30,  # 降低帧率以减小文件大小
                program="ffmpeg",  # 使用ffmpeg可能会得到更好的性能
                opt="nq",  # 启用优化选项
            )
        return f"Success: Converted {filename} to {gif_filename}"
    except Exception as e:
        return f"Error: {filename} - {str(e)}"


def main():
    # 检查输入文件夹是否存在
    if not os.path.exists(input_folder):
        print(f"Error: Input folder '{input_folder}' does not exist")
        return

    # 获取所有支持的视频文件
    video_files = [
        f
        for f in os.listdir(input_folder)
        if f.lower().endswith((".mp4", ".avi", ".mov", ".mkv"))
    ]

    if not video_files:
        print("No supported video files found in the input folder.")
        return

    print(f"Found {len(video_files)} video files to process...")

    # 使用 ThreadPoolExecutor 来进行多线程处理
    with ThreadPoolExecutor() as executor:
        # 提交所有转换任务
        future_to_filename = {
            executor.submit(convert_video_to_gif, filename): filename
            for filename in video_files
        }

        # 获取结果
        for future in as_completed(future_to_filename):
            result = future.result()
            print(result)

    print("Processing complete.")


if __name__ == "__main__":
    main()
