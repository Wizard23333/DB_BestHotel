<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>房间系统</el-breadcrumb-item>
      <el-breadcrumb-item :to="{ path: '/roomType' }">房间类型</el-breadcrumb-item>
      <el-breadcrumb-item>添加房间类型</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片视图 -->
    <el-card>
      <!-- 提示 -->
      <el-alert title="添加房间类型" type="info" center show-icon :closable="false"></el-alert>
      <!-- 步骤条 -->
      <el-steps :space="200" :active="activeIndex - 0" finish-status="success" align-center>
        <el-step title="基本信息"></el-step>
        <el-step title="房间图片url"></el-step>
        <el-step title="房间文案"></el-step>
        <el-step title="完成"></el-step>
      </el-steps>

      <!-- Tab栏 -->
      <el-form
        :model="addForm"
        :rules="addFormRules"
        ref="addFormRef"
        label-width="100px"
        label-position="top"
      >
        <el-tabs
          v-model="activeIndex"
          :tab-position="'left'"
          :before-leave="beforeTabLeave"
         
        >
          <el-tab-pane label="基本信息" name="0">
            <el-form-item label="房间类型" prop="room_type">
              <el-input v-model="addForm.room_type"></el-input>
            </el-form-item>
          
          </el-tab-pane>
         
          
          <el-tab-pane label="房间图片url" name="2">
            <el-form-item label="房间图片url" prop="room_url">
              <el-input v-model="addForm.room_url" ></el-input>
            </el-form-item>
            
          </el-tab-pane>
          <el-tab-pane label="房间文案" name="3">
            <el-form-item label="房间文案" prop="room_explain">
              <el-input v-model="addForm.room_explain"></el-input>
            </el-form-item>
             
            <!-- 添加 -->
            <el-button type="primary" class="btnAdd" @click="addroom">添加</el-button>
          </el-tab-pane>
        </el-tabs>
      </el-form>
    </el-card>
   <!-- <el-dialog title="图片预览" :visible.sync="previewDialogVisible" width="50%">
      <img :src="picPreviewPath" alt="" class="previewImg">
    </el-dialog>-->
  </div>
</template>

<script>
import _ from 'lodash'
export default {
  data () {
    return {
      // 步骤条默认激活 与左侧Tab联动
      activeIndex: '0',
      // 添加商品的表单对象
      addForm: {
        room_type: '',
        room_price: 0,
        // 图片的数组
        room_url: '',  
        room_explain: ''
       
      },
      addFormRules: {
        room_type: [
          { required: true, message: '请输入房间类型', trigger: 'blur' }
        ],
        room_url: [
          { required: true, message: '请输入房间图片url', trigger: 'blur' }
        ],
        room_explain: [
          { required: true, message: '请输入房间文案', trigger: 'blur' }
        ]
      },
    
    }
  },
  
  methods: {
   
    
    
    beforeTabLeave (activeName, odlActiveName) {
      //未选中商品分类阻止Tab标签跳转
      if (odlActiveName === '0' && this.addForm.room_type === "") {
        this.$message.error('请先输入房间类型')
        return false
      }
    },
    //Tab标签被选中时触发
   
   
    // 添加
    addroom () {
       console.log(222);
      this.$refs.addFormRef.validate(async valid => {
        if (!valid) return this.$message.error('请填写必要的表单项！')
        // 发送请求前：需对提交的表单进行处理room_cat attrs
        // this.addForm.room_cat = this.addForm.room_cat.join(',')
        // 以上写法不对：级联选择器绑定的对象room_cat要求是数组对象
        // 解决办法: 包：lodash 方法（深拷贝）：cloneDeep(boj)

        // 将this.addForm进行深拷贝
        const form = _.cloneDeep(this.addForm)
        console.log(form);
        const { data: res } = await this.$http.post('Room/RoomTypeAdd?room_type='+form.room_type+'&room_url='+form.room_url+'&room_explain='+form.room_explain  )
        console.log(res);
        if (res.code !== 200) return this.$message.error('添加房间类型失败！')
        this.$message.success('添加房间类型成功!')
        this.$router.push('/roomType')
      })
    }
  }
}
</script>

<style scoped>
.el-checkbox {
  margin: 0 8px 0 0 !important;
}
.previewImg{
  width: 100%;
}
.btnAdd{
  margin-top: 15px
}
</style>
