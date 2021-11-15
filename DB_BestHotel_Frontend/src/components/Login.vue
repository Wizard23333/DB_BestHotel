<template>
  <div class="login_container">
    <div>
      <el-dialog
        title="顾客注册"
        :visible.sync="dialogFormVisibleClient"
        width="80%"
      >
        <el-form
          label-position="left"
          label-width="150px"
          :model="clientForm"
          :rules="clientFormRules"
          ref="clientRegisterRef"
        >
          <el-form-item label="客户ID：" prop="client_id">
            <el-input v-model="clientForm.client_id"></el-input>
          </el-form-item>
          <el-form-item label="用户名：" prop="client_name">
            <el-input v-model="clientForm.client_name"></el-input>
          </el-form-item>
          <!-- <el-form-item label="性别：" >
            <el-input v-model="clientForm.client_sex"></el-input>
          </el-form-item> -->
          <el-form-item label="性别：" prop="client_sex">
            <!-- <el-input v-model="staffForm.staff_sex"></el-input> -->
            <el-select
              v-model="clientForm.client_sex"
              placeholder="请选择您的性别"
            >
              <el-option label="男" value="男"></el-option>
              <el-option label="女" value="女"></el-option>
            </el-select>
          </el-form-item>
          <!-- <el-form-item label="生日：" >
            <el-input v-model="clientForm.client_birthday"></el-input>
          </el-form-item> -->
          <el-form-item label="生日：" prop="client_birthday">
            <el-date-picker
              type="date"
              placeholder="选择输入您的生日"
              value-format="yyyy-MM-dd"
              v-model="clientForm.client_birthday"
              style="width: 100%"
            ></el-date-picker>
          </el-form-item>

          <el-form-item label="电话号码：" prop="client_mobile">
            <el-input v-model="clientForm.client_mobile"></el-input>
          </el-form-item>
          <el-form-item label="身份证号码：" prop="client_idCard">
            <el-input v-model="clientForm.client_idCard"></el-input>
          </el-form-item>
          <el-form-item label="密码：" prop="password">
            <el-input v-model="clientForm.password"></el-input>
          </el-form-item>
          <el-form-item label="密保问题：" prop="security_q">
            <el-input v-model="clientForm.security_q"></el-input>
          </el-form-item>
          <el-form-item label="密保答案：" prop="s_q_answer">
            <el-input v-model="clientForm.s_q_answer"></el-input>
          </el-form-item>

          <el-form-item>
            <el-button
              type="primary"
              style="position: absolute; width: 100"
              @click="saveClient()"
              >保存</el-button
            >
          </el-form-item>
        </el-form>
      </el-dialog>
      <el-dialog
        title="员工注册"
        :visible.sync="dialogFormVisibleStaff"
        width="80%"
      >
        <el-form
          label-position="left"
          label-width="160px"
          :rules="staffFormRules"
          ref="staffRegisterRef"
          :model="staffForm"
        >
          <el-form-item label="员工ID：" prop="staff_id">
            <el-input v-model="staffForm.staff_id"></el-input>
          </el-form-item>
          <el-form-item label="员工姓名：" prop="staff_name">
            <el-input v-model="staffForm.staff_name"></el-input>
          </el-form-item>
          <!-- <el-form-item label="性别：">
            <el-input v-model="staffForm.staff_sex"></el-input>
          </el-form-item> -->
          <el-form-item label="性别：" prop="staff_sex">
            <!-- <el-input v-model="staffForm.staff_sex"></el-input> -->
            <el-select
              v-model="staffForm.staff_sex"
              placeholder="请选择您的性别"
            >
              <el-option label="男" value="男"></el-option>
              <el-option label="女" value="女"></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="年龄：" prop="staff_age">
            <el-input v-model.number="staffForm.staff_age"></el-input>
          </el-form-item>
          <el-form-item label="身份证号码：" prop="staff_identity_card_number">
            <el-input v-model="staffForm.staff_identity_card_number"></el-input>
          </el-form-item>
          <el-form-item label="住址：" prop="staff_address">
            <el-input v-model="staffForm.staff_address"></el-input>
          </el-form-item>
          <el-form-item label="部门：" prop="staff_department">
            <el-input v-model="staffForm.staff_department"></el-input>
          </el-form-item>
          <el-form-item label="职位：" prop="staff_position">
            <el-input v-model="staffForm.staff_position"></el-input>
          </el-form-item>
          <el-form-item label="入职日期：" prop="staff_entry_date">
            <el-date-picker
              type="date"
              placeholder="选择入职日期"
              value-format="yyyy-MM-dd"
              v-model="staffForm.staff_entry_date"
              style="width: 100%"
            ></el-date-picker>
          </el-form-item>
          <el-form-item label="用户密码：" prop="password">
            <el-input v-model="staffForm.password"></el-input>
          </el-form-item>
          <el-form-item label="密保问题：" prop="security_q">
            <el-input v-model="staffForm.security_q"></el-input>
          </el-form-item>
          <el-form-item label="密保答案：" prop="s_q_answer">
            <el-input v-model="staffForm.s_q_answer"></el-input>
          </el-form-item>
          <el-form-item label="身份验证：" prop="staff_secret_key">
            <el-input v-model="staffForm.staff_secret_key"></el-input>
          </el-form-item>

          <el-form-item>
            <el-button
              type="primary"
              style="position: absolute; width: 100"
              @click="saveStaff()"
              >保存</el-button
            >
          </el-form-item>
        </el-form>
      </el-dialog>
      <el-dialog title="身份选择" :visible.sync="dialogFormVisible" width="30%">
        <el-form label-position="left" label-width="160px">
          <br /><br />
          <el-button
            type="primary"
            style="text-align: center; margin-bottom: 30px; width: 120px"
            @click="addClient"
            >顾客
          </el-button>
          <el-button
            type="primary"
            @click="addStaff"
            style="
              text-align: center;
              float: right;
              margin-bottom: 30px;
              width: 120px;
            "
            >员工</el-button
          >
          <br /><br />
        </el-form>
      </el-dialog>
      <el-dialog
        title="请输入您的ID"
        :visible.sync="dialogFormVisibleRetrieve"
        width="30%"
      >
        <el-form
          label-position="left"
          label-width="100px"
          :model="getSQForm"
          :rules="getSQFormRules"
          ref="getSQRef"
        >
          <el-form-item label="用户ID：" prop="user_id">
            <el-input v-model="getSQForm.user_id"></el-input>
          </el-form-item>

          <el-form-item
            ><el-button
              type="primary"
              @click="confirmID"
              style="text-align: center; float: right; width: 120px"
              >提交</el-button
            ></el-form-item
          >
        </el-form>
      </el-dialog>
      <el-dialog
        title="请验证您的密保问题并输入新密码："
        :visible.sync="dialogFormVisibleConfirm"
        width="50%"
      >
        <el-form
          label-position="left"
          label-width="170px"
          :model="newPwdForm"
          :rules="newPwdFormRules"
          ref="newPwdRef"
        >
          <el-form-item label="您的密保问题为：">
            <el-input
              readonly="true"
              v-model="getSQForm.security_q"
              :disabled="true"
            ></el-input>
          </el-form-item>
          <el-form-item label="请输入您的密保答案：" prop="s_q_answer">
            <el-input
              v-model="newPwdForm.s_q_answer"
              placeholder="密保答案"
            ></el-input>
          </el-form-item>
          <el-form-item label="请输入您的新密码：" prop="user_password">
            <el-input
              v-model="newPwdForm.user_password"
              placeholder="新密码"
            ></el-input>
          </el-form-item>

          <el-form-item
            ><el-button
              type="primary"
              @click="updatePwd"
              style="text-align: center; float: right; width: 120px"
              >提交</el-button
            ></el-form-item
          >
        </el-form>
      </el-dialog>
    </div>
    <div>
      <div class="login_box">
        <div class="avator_box">
          <img src="../assets/logo.png" alt="" />
        </div>
        <!-- 登陆表单 -->
        <el-form
          ref="loginFormRef111"
          :model="loginForm"
          :rules="loginFormRules"
          label-width="0px"
          class="login_form"
        >
          <el-form-item prop="user_id">
            <el-input
              v-model="loginForm.user_id"
              prefix-icon="el-icon-user-solid"
              placeholder="请输入您的用户ID"
            ></el-input>
          </el-form-item>
          <el-form-item prop="user_password">
            <el-input
              type="password"
              v-model="loginForm.user_password"
              prefix-icon="el-icon-unlock"
              placeholder="请输入您的用户密码"
            ></el-input>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" style="width: 100%" @click="login"
              >登录</el-button
            >
          </el-form-item>
          <!-- <div class="tips">
        <span style="margin-right: 20px">user_id: admin</span>
        <span> password: any</span>
      </div> -->

          <el-form-item>
            <el-button
              type="success"
              @click="goRegister"
              style="width: 48.55%; margin-left: 0px"
              >去注册</el-button
            >
            <el-button type="info" @click="goFindPwd" style="width: 48.55%"
              >忘记密码？</el-button
            >
          </el-form-item>

          <!-- <el-form-item class="btns">
          <el-button type="primary" @click="login">登陆</el-button>
          <el-button type="info" @click="resetLoginForm">重置</el-button>
        </el-form-item> -->
        </el-form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialogFormVisible: false,
      dialogFormVisibleClient: false,
      dialogFormVisibleStaff: false,
      dialogFormVisibleRetrieve: false,
      dialogFormVisibleConfirm: false,

      getSQForm: {
        user_id: "",
        security_q: "",
      },

      getSQFormRules: {
        user_id: [
          {
            required: true,
            message: "需要您的用户ID来进行下一步",
            trigger: "blur",
          },
        ],
        security_q: [],
      },

      newPwdForm: {
        s_q_answer: "",
        user_password: "",
      },
      newPwdFormRules: {
        s_q_answer: [
          {
            required: true,
            message: "请输入您的密保答案",
            trigger: "blur",
          },
        ],
        user_password: [
          {
            required: true,
            message: "请输入您的新密码",
            trigger: "blur",
          },
        ],
      },

      clientForm: {
        client_id: "",
        client_name: "",
        client_sex: "",
        client_birthday: "",
        client_mobile: "",
        client_idCard: "",
        password: "",
        security_q: "",
        s_q_answer: "",
      },
      clientFormRules: {
        client_id: [
          {
            required: true,
            message: "请设置您的客户ID",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        client_name: [
          {
            required: true,
            message: "请输入用户名",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        client_sex: [
          { required: true, message: "请输入您的性别", trigger: "blur" },
          {
            type: "enum",
            enum: ["男", "女"],
            message: "性别只能是男和女",
            trigger: "blur",
          },
        ],
        client_birthday: [
          { required: true, message: "请输入您的生日", trigger: "blur" },
        ],
        client_mobile: [
          { required: true, message: "请输入您的手机号", trigger: "blur" },
        ],
        client_idCard: [
          { required: true, message: "请输入您的身份证号", trigger: "blur" },
          {
            len: 18,
            message: "请输入有效的身份证号",
            trigger: "blur",
          },
        ],
        password: [
          {
            required: true,
            message: "请输入需要设置的密码",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        security_q: [
          {
            required: true,
            message: "请输入需要设置的密保问题",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        s_q_answer: [
          {
            required: true,
            message: "请输入需要设置的密保答案",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
      },
      staffForm: {
        staff_id: "",
        staff_name: "",
        staff_sex: "",
        staff_age: "",
        staff_identity_card_number: "",
        staff_address: "",
        staff_department: "",
        staff_position: "",
        staff_entry_date: "",
        password: "",
        security_q: "",
        s_q_answer: "",
        staff_secret_key: "",
      },
      staffFormRules: {
        staff_id: [
          {
            required: true,
            message: "请设置您的员工ID",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10个字符",
            trigger: "blur",
          },
        ],
        staff_name: [
          { required: true, message: "请输入您的姓名", trigger: "blur" },
          {
            max: 10,
            message: "长度不能大于10个字符",
            trigger: "blur",
          },
        ],
        staff_sex: [
          { required: true, message: "请输入您的性别", trigger: "blur" },
          {
            type: "enum",
            enum: ["男", "女"],
            message: "性别只能是男和女",
            trigger: "blur",
          },
        ],
        staff_age: [
          { required: true, message: "请输入您的年龄", trigger: "blur" },
          {
            type: "number",
            min: 16,
            max: 65,
            message: "年龄需要在16～65周岁之间",
            trigger: "blur",
          },
        ],
        staff_identity_card_number: [
          { required: true, message: "请输入您的身份证号码", trigger: "blur" },
          {
            len: 18,
            message: "请输入有效的身份证号",
            trigger: "blur",
          },
        ],
        staff_address: [
          { required: true, message: "请输入您的住址", trigger: "blur" },
          {
            max: 30,
            message: "长度不能大于30",
            trigger: "blur",
          },
        ],
        staff_department: [
          { required: true, message: "请输入您的部门", trigger: "blur" },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        staff_position: [
          { required: true, message: "请输入您的职位", trigger: "blur" },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        staff_entry_date: [
          { required: true, message: "请输入您的入职日期", trigger: "blur" },
        ],
        password: [
          {
            required: true,
            message: "请输入需要设置的密码",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        security_q: [
          {
            required: true,
            message: "请输入需要设置的密保问题",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        s_q_answer: [
          {
            required: true,
            message: "请输入需要设置的密保答案",
            trigger: "blur",
          },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        staff_secret_key: [
          {
            required: true,
            message: "请输入需要您的邀请密钥",
            trigger: "blur",
          },
        ],
      },
      //数据绑定对象
      loginForm: {
        user_id: "",
        user_password: "",
      },
      //表单验证对象
      loginFormRules: {
        //用户名
        user_id: [
          { required: true, message: "请输入用户ID", trigger: "blur" },
          {
            min: 2,
            message: "长度需要大于2个字符",
            trigger: "blur",
          },
        ],

        user_password: [
          { required: true, message: "请输入用户密码", trigger: "blur" },
          {
            min: 6,
            max: 18,
            message: "长度在 6 到 18 个字符",
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    saveClient() {
      this.$refs.clientRegisterRef.validate(async (validTag) => {
        if (!validTag) return;

        console.log(this.clientForm);
        const res = await this.$http.post(
          "Login/Register/Client",
          this.clientForm
        );
        console.log(res);
        if (res.status !== 200) return this.$message.error("获取数据失败");
        if (res.data.code !== 200) {
          return this.$message.error("注册失败");
        } else {
          this.$message.success("注册成功");
        }
        this.dialogFormVisibleClient = false;
      });
    },
    addClient() {
      this.dialogFormVisible = false;
      this.dialogFormVisibleClient = true;

      this.$nextTick(() => {
        this.$refs["clientRegisterRef"].clearValidate(); //重置表单状态
      });
    },
    saveStaff() {
      this.$refs.staffRegisterRef.validate(async (validTag) => {
        if (!validTag) return;
        var tempStaffForm = {
          staff_id: this.staffForm.staff_id,
          staff_name: this.staffForm.staff_name,
          staff_sex: this.staffForm.staff_sex,
          staff_age: this.staffForm.staff_age.toString(),
          staff_identity_card_number: this.staffForm.staff_identity_card_number,
          staff_address: this.staffForm.staff_address,
          staff_department: this.staffForm.staff_department,
          staff_position: this.staffForm.staff_position,
          staff_entry_date: this.staffForm.staff_entry_date,
          staff_salary: "10000",
          password: this.staffForm.password,
          security_q: this.staffForm.security_q,
          s_q_answer: this.staffForm.s_q_answer,
        };

        if (this.staffForm.staff_secret_key !== "iamastaff")
          return this.$message.error("注册失败");

        console.log(tempStaffForm);
        const res = await this.$http.post(
          "Login/Register/Staff",
          tempStaffForm
        );
        console.log(res);

        if (res.status !== 200) return this.$message.error("获取数据失败");
        if (res.data.code !== 200) {
          this.$message.success("注册失败");
        } else {
          this.$message.success("注册成功");
        }
        this.dialogFormVisibleStaff = false;
      });
    },
    async addStaff() {
      this.dialogFormVisible = false;
      this.dialogFormVisibleStaff = true;

      this.$nextTick(() => {
        this.$refs["staffRegisterRef"].clearValidate(); //重置表单状态
      });
    },
    goRegister() {
      //this.$router.push({ path: '/clientRegister' }),
      // console.log(1356);
      this.dialogFormVisible = true;
    },
    goFindPwd() {
      this.dialogFormVisibleRetrieve = true;
      // this.$router.push({ path: "/goFindPwd" });
      this.$nextTick(() => {
        this.$refs["getSQRef"].clearValidate(); //重置表单状态
      });
    },
    confirmID() {
      this.$refs.getSQRef.validate(async (validTag) => {
        if (!validTag) return;

        const res = await this.$http.get(
          "Login/Forget/" + this.getSQForm.user_id
        );
        if (res.status !== 200) return this.$message.error("获取数据失败");
        if (res.data.code !== 200 || res.data.data.security_q.length == 0) {
          return this.$message.error("获取失败");
        } else {
          this.$message.success("获取成功");
        }
        console.log(res);
        this.getSQForm.security_q = res.data.data.security_q;
        this.dialogFormVisibleRetrieve = false;
        this.dialogFormVisibleConfirm = true;

        this.newPwdForm.s_q_answer = "";
        this.newPwdForm.user_password = "";

        this.$nextTick(() => {
          this.$refs["newPwdRef"].clearValidate(); //重置表单状态
        });
      });
    },

    async updatePwd() {
      this.$refs.newPwdRef.validate(async (validTag) => {
        if (!validTag) return;

        // console.log(this.getSQForm.user_id);
        console.log(this.newPwdForm);
        const res = await this.$http.post(
          "Login/Forget/" + this.getSQForm.user_id,
          this.newPwdForm
        );

        if (res.status !== 200) return this.$message.error("获取数据失败");
        if (res.data.code !== 200) {
          return this.$message.error("重置失败");
        } else {
          this.$message.success("重置成功");
        }
        this.dialogFormVisibleConfirm = false;
      });
    },
    //点击重置按钮
    resetLoginForm() {
      console.log(this);
      // resetFields：element-ui提供的表单方法
      this.$refs.loginFormRef111.resetFields();
    },
    login() {
      // 获取引用 验证
      this.$refs.loginFormRef111.validate(async (validTag) => {
        //判断bool形参来预验证登陆
        // console.log(validTag);
        if (!validTag) return;
        console.log(this.loginForm);
        const res = await this.$http.post("Login", this.loginForm);
        console.log(res);
        if (res.status !== 200) return this.$message.error("获取数据失败");
        if (res.data.code !== 200) {
          return this.$message.error("登陆失败");
        } else {
          this.$message.success("登陆成功");
        }
        console.log(res.data.data);

        window.sessionStorage.setItem("user_id", res.data.data.user_id);
        window.sessionStorage.setItem("user_name", res.data.data.user_name);
        window.sessionStorage.setItem("user_type", res.data.data.user_type);

        //解构data属性为result
        // const result = await this.$http.get("/try/ajax/json_demo.json");
        // console.log(result);
        // console.log(result.status);

        // 1、将登陆成功之后的token, 保存到客户端的sessionStorage中; localStorage中是持久化的保存
        //   1.1 项目中出现了登录之外的其他API接口，必须在登陆之后才能访问
        //   1.2 token 只应在当前网站打开期间生效，所以将token保存在sessionStorage中

        // window.sessionStorage.setItem('token', res.data.token)

        window.sessionStorage.setItem("token", 12313);
        // 2、通过编程式导航跳转到后台主页, 路由地址为：/home
        // this.$router.push("/home");
        if(res.data.data.user_type==='staff')
        {this.$router.push("/home");}
        if(res.data.data.user_type==='client')
        {this.$router.push("/home_cli/");}
      });
    },
    // isvalidPhone(str) {
    //   const reg = /^1[3|4|5|7|8][0-9]\d{8}$/;
    //   return reg.test(str);
    // },
  },
};
</script>


<style lang="less" scoped>
.login_container {
  background-color: #2D3A4B;
  height: 100%;
}

.title-container {
  position: relative;

  .title {
    font-size: 30px;
    color: #2D3A4B;
    // color: $light_gray;
    margin: 20px auto 40px auto;
    margin-bottom: 10px;
    text-align: center;
    font-weight: bold;
  }
}

.login_box {
  width: 450px;
  height: 280px;
  background-color: #e4e3e3;
  border-radius: 5px;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}

.avator_box {
  width: 409px;
  height: 130px;
  border-radius: 5%;
  padding: 10px;
  position: absolute;
  left: 50%;
  top: -22%;
  transform: translate(-50%, -50%);
  img {
    width: 100%;
    height: 100%;
    border-radius: 5%;
  }
}

.login_form {
  position: absolute;
  bottom: 0px;
  width: 100%;
  padding: 0 20px;
  box-sizing: border-box;
}

.btns {
  display: flex;
  justify-content: flex-end;
}
</style>