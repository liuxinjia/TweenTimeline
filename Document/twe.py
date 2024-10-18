import os
from moviepy.editor import VideoFileClip

# 获取当前脚本所在的文件夹路径
current_folder = os.path.dirname(os.path.abspath(__file__))

# 设置视频文件夹和输出 GIF 文件夹
input_folder = os.path.join(current_folder, "videos")  # 输入文件夹名
output_folder = os.path.join(current_folder, 'gifs')    # 输出文件夹名

# 创建输出文件夹（如果不存在）
os.makedirs(output_folder, exist_ok=True)

# 遍历输入文件夹中的所有文件
for filename in os.listdir(input_folder):
    if filename.endswith(('.mp4', '.avi', '.mov', '.mkv')):  # 支持的视频格式
        video_path = os.path.join(input_folder, filename)
        gif_filename = os.path.splitext(filename)[0] + '.gif'  # GIF 文件名
        gif_path = os.path.join(output_folder, gif_filename)

        # 加载视频并转换为 GIF
        clip = VideoFileClip(video_path)
        clip.write_gif(gif_path)
        print(f'Converted {filename} to {gif_filename}')

print('All videos have been converted to GIFs.')
