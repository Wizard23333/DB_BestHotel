
<template>
  <div class="tableDiv">
     <!-- <el-table :data="userList" border stripe>
        <el-table-column label="我的ID" prop="staff_id"> </el-table-column>
        <el-table-column label="我的姓名" prop="staff_name"> </el-table-column>
        <el-table-column label="我的领导" prop="leader_id"> </el-table-column>
        <el-table-column label="我的职位" prop="staff_position"> </el-table-column>
        <el-table-column label="我的部门" prop="staff_department"> </el-table-column>
        <el-table-column label="入职时间" prop="staff_entry_date"> </el-table-column>
        <el-table-column label="薪水" prop="staff_salary"> </el-table-column>
        <el-table-column label="性别" prop="staff_sex"> </el-table-column>
        <el-table-column label="年龄" prop="staff_age"> </el-table-column>
       
        <el-table-column label="身份证号码" prop="staff_idCard"></el-table-column>
        
      </el-table>-->
      <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>账户信息</el-breadcrumb-item>
    </el-breadcrumb>
      <el-form
          label-position="left"
          label-width="150px"
      
          :model="staffForm"
          ref="loginFormRef111"
        >
        <el-form-item label="ID：" prop="staff_id">
            <el-input v-model="staffForm.staff_id" 
              readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="姓名：" prop="staff_name">
            <el-input v-model="staffForm.staff_name" 
              readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="性别：" prop="staff_sex">
            <el-input v-model="staffForm.staff_sex" readonly="true"
              :disabled="true"></el-input> 
            
          </el-form-item>

          <el-form-item label="年龄：" prop="staff_age">
            <el-input v-model.number="staffForm.staff_age" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item
            label="身份证号码："
            prop="staff_identity_card_number"
          >
            <el-input v-model="staffForm.staff_identity_card_number" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="住址：" prop="staff_address">
            <el-input v-model="staffForm.staff_address" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="所属部门：" prop="staff_department">
            <el-input v-model="staffForm.staff_department" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="上司id：" prop="leader_id">
            <el-input v-model="staffForm.leader_id" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="职位：" prop="staff_position">
            <el-input v-model="staffForm.staff_position" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

          <el-form-item label="入职时间：" prop="staff_entry_date">
            <el-input v-model="staffForm.staff_entry_date" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>
          <el-form-item label="薪水：" prop="staff_salary">
            <el-input v-model="staffForm.staff_salary" readonly="true"
              :disabled="true"></el-input>
          </el-form-item>

         
           
        </el-form>
   
  </div>
</template>
<script>
export default {
  data() {
    return {
    
     staffForm: [],

      //获取用户信息的参数对象
      // queryClientInfo: {
      //   query: "",
      //   //client_id:ruleFrom.client_id
      // },
      // editDialogVisible:false,
     

    };
  },
  created() {
    //this.getClientInfo();
    this.getUserList();
  },
  methods: {
    async getUserList() {
      var temp={ staff_id:  window.sessionStorage.getItem("user_id")}
      const {data: res}=await this.$http.post("Staff/StaffInfo",temp)
      
      if (res.code != 200) {
        return this.$message.error("获取信息失败");
      }
       console.log(res)
      //console.log(res);
      this.staffForm = res.data;
      console.log(2222)
      console.log (this.staffForm)
      console.log(2222)
    },
    
  },
};
</script>
<style lang="less" scoped>
</style>

