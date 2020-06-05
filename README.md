# 软件编写使用说明
## 有用的资源
1. [免费图标下载网站](https://icons8.com/)  最小的一般是32*32大小
## 主界面
## 窗口
1. 程序名字
2. 程序图标
注：资源里面的图片格式为bitmap，所以要转为icon才能作为图标，可参考该程序
### 标题
设置内容在From1.cs的初始化中
可设置内容包括：
1. 字体和大小
2. 颜色
3. 标题内容
### 菜单栏
1. 菜单按钮字体
2. 菜单按钮高度
3. 菜单栏宽度
4. 菜单按钮以List<Tuple<MenuButton, bool>> MenuButtons进行管理,其中MenuButton是单个菜单按钮，bool表示该按钮是放在上方（True）还是放在下方（False）
5. 菜单按钮类是./Core/custom_class/MenuButton.cs
