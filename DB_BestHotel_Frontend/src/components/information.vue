<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>我的信息</el-breadcrumb-item>
    </el-breadcrumb>
    <el-card>
     
      <el-table :data="userList" border stripe>
        <el-table-column label="我的ID" prop="client_id"> </el-table-column>
        <el-table-column label="我的姓名" prop="client_name"> </el-table-column>
        <el-table-column label="性别" prop="client_sex"> </el-table-column>
        <el-table-column label="生日" prop="client_birthday"> </el-table-column>
        <el-table-column label="手机号码" prop="client_mobile"> </el-table-column>
        <el-table-column
          label="身份证号码"
          prop="client_idCard"
        ></el-table-column>
        <el-table-column label="操作" width="100px">
          <template slot-scope="scope">
            <el-tooltip
              effect="dark"
              content="修改信息"
              placement="top"
              :enterable="false"
            >
              <el-button
                type="primary"
                icon="el-icon-edit"
                @click="showEditDialog(scope.row.client_id)"
              ></el-button>
            </el-tooltip>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
    <el-dialog
      title="修改用户"
      :visible.sync="editDialogVisible"
      width="50%"
      
    >
      <el-form :model="editForm" :rules="editFormRules" ref="editFormRef" label-width="70px">
  <el-form-item label="用户ID">
    <el-input v-model="editForm.client_id" disabled></el-input>
  </el-form-item> 
  <el-form-item label="手机号" prop="client_mobile">
    <el-input v-model="editForm.client_mobile"></el-input>
  </el-form-item>

      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="editDialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="editUserInfo"
          >确 定</el-button
        >
      </span>
    </el-dialog>
  </div>
</template>
<script>
export default {
  data() {
    var checkMobile=(rule,value,cb)=>{
      const regMobile =/^(0|86|17951)?(13[0-9]|15[0123456789]|17[678]|18[0-9]|14[57])[0-9]{8}$/
      if(regMobile.test(value)){
        return cb()
      }
      cb(new Error('请输入合法手机号'))
    }
    return {
      queryInfo: {
        //query: "",
       
      },
      
     status:0,
      userList:[],
      //获取用户信息的参数对象
      // queryClientInfo: {
      //   query: "",
      //   //client_id:ruleFrom.client_id
      // },
      // editDialogVisible:false,
      editDialogVisible:false,
      editForm:{},
      client_id:"111",
      editFormRules:{
        client_mobile:[
          {required:true,message:'请输入电话号码' , trigger:'blur'},
          {validator:checkMobile,trigger:'blur'},
         // {validator:checkMobile,trigger:'blur'}
        ]
      }

    };
  },
  created() {
    //this.getClientInfo();
    this.getUserList();
  },
  methods: {
    
    async getUserList() {
      //console.log(111)
      let id = sessionStorage.getItem('user_id');
      const { data: res } = await this.$http.get("Client/"+id, {
        params: "this.queryInfo",
      });
      
      if (res.code != 200) {
        return this.$message.error("失败");
      }
   
      console.log(res.data);
      //console.log(111)

      this.userList = [res.data];
      //console.log(this.userList.client_id)
     // console.log(222)
    },
    //修改对话框
    async showEditDialog(id) {
      const {data: res}=await this.$http.get('Client/'+id)
      if(res.code!=200){
        return this.$message.error("请求失败！")
      }
      this.editForm=res.data
      //console.log(this.editForm)
      this.editDialogVisible=true
    },
    editUserInfo(){
        this.$refs.editFormRef.validate(async valid=>{
          if(!valid) return 
          const{data:res}=await this.$http.post('Client',{client_id:this.editForm.client_id,client_mobile:this.editForm.client_mobile
          })
          console.log(this.editForm)
          if(res.code!=200){
            return this.$message.error('更新失败！')
          }
          this.editDialogVisible=false
          this.getUserList()
          this.$message.success('修改成功')
        })
    },
  },
};
</script>
<style lang="less" scoped>
</style>