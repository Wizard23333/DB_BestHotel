# 百思特酒店系统

## 前端项目运行方法

**1. 搭建Node.js环境：**

- 访问https://nodejs.org/en/下载最新版NodeJS环境即可(选择左侧14.17.0 LTS即可)

<img src="D:\Projects\DB_BestHotel\README.assets\image-20210721221034226.png" alt="image-20210721221034226" style="zoom:50%;" />

​	安装过程推荐自行搜索，太复杂的安装过程请慎重选择

**2. 全局安装Webpack**

- Win+R键后输入cmd，回车打开控制台，输入：

  ```shell
  npm install webpack -g
  ```

**3. 安装Vue脚手架**

- Win+R键后输入cmd，回车打开控制台，输入：

  ```shell
  npm install -g @vue/cli
  ```

- 验证是否安装成功，控制台输入：

  ```shell
  vue --version
  ```

- 显示vue版本即安装成功：

  ```
  @vue/cli 4.5.13
  ```

**4. 运行项目**

+ 直接使用cmd运行

  1. `cd`进入`DB_BestHotel_Frontend`文件夹

     ```shell
     cd/d D:\***\Projects\DB_BestHotel
     ```

     ***(换用成自己的路径)

  2. 安装依赖

     ```shell
     npm install
     ```

  3. 运行

     ```shell
     npm run serve
     ```

+ 使用VSCode中的终端运行(方便热修改以及调试)
  1. 用VSCode打开项目，启动其终端
  2. 重复上一个方法的2~3步

**5. 注意事项**

​	<span style = "color: red">前端项目暂时无法离线运行，需要修改登录代码才能看到后续界面 </span>







