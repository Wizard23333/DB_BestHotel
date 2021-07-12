<template>
    <div class="ogin_container">
        <div class ="login_box">
            <el-form ref="loginFormRef" :model="loginForm" :rules="loginFormRules" label-width="0px">
  <el-form-item prop="username" >
    <el-input v-model="loginForm.username"></el-input>
  </el-form-item>
  <el-form-item prop="password" >
    <el-input v-model="loginForm.password" type="password"></el-input>
  </el-form-item>
  <el-form-item >
    <el-button type="primary" @click="login">登录</el-button>
    <el-button type="info" @click="resetLoginForm">重置</el-button>
  </el-form-item>
            </el-form>
        </div>
    </div>  
</template>
<script>
export default{
    data(){
        return {
            loginForm:{
                username:'admin',
                password:'123456'
            },
            loginFormRules:{
                username:[{ required: true, message: '请输入登录名称', trigger: 'blur' },
                          { min: 3, max: 10, message: '长度在 3 到 10 个字符', trigger: 'blur' }],
                password:[{ required: true, message: '请输入登录密码', trigger: 'blur' },
                          { min: 6, max: 15, message: '长度在 6 到 15 个字符', trigger: 'blur' }]
            }
        };
    },
    methods:{
        resetLoginForm(){
            this.$refs.loginFormRef.resetFields();
        },
        login(){
            this.$refs.loginFormRef.validate(async valid=>{
                if(!valid) return 
                const res=await this.$http.post("login",this.loginForm);
                console.log(res);
                window.sessionStorage.setItem('token',res.data.data.token);
                console.log(res.data.data.id);
                console.log(res.data.data.token);
               // });
            this.$router.push('/orderTable');
            })  
        
    }}
};
</script>

<style lang='less' scoped>
.login_container{
    background-color: #2b4b6b;
    height: 100%;
}

.login_box{
    width: 450px;
    height:300px;
    background-color: rgb(107, 37, 37);
    border-radius: 3px;
    position:absolute;
    left:50%;
    right:50%;
}


</style>