<template >
  <div class="hello">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>房间系统</el-breadcrumb-item>
      <el-breadcrumb-item>房间类型</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="searchDiv">
    
      <!-- 模态框 -->
     
        <el-button type="success" @click="goAddPage">新添房间类型</el-button>
      
      <el-dialog title="输入房间信息" :visible.sync="dialogFormVisible">
        <el-form>
          
          <el-form-item label="房间文案" :label-width="formLabelWidth">
            <el-input v-model="roomExplain"></el-input>
          </el-form-item>


        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="add(roomExplain)">保存</el-button>
        </div>
      </el-dialog>
    </div>
    <div class="tableDiv">
    
      <el-table :data="typeList" style="width: 100%" ref="myTable">
        <el-table-column type="index" :index="indexMethod"></el-table-column>
        <el-table-column prop="room_type" label="房间类型" width="180"></el-table-column>
        <el-table-column prop="room_price"  label="房间价格"></el-table-column>
         <el-table-column prop="room_explain" label="房间文案"></el-table-column>
        <el-table-column prop="address" label="操作">
          <template slot-scope="scope">
          <el-button type="primary" @click="edit(scope.$index,scope.row)">修改</el-button>
          <el-button type="danger" @click="dele(scope.$index,scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      input: "",
      roomPrice: "",
      roomExplain: "",
      roomType: "",
      dialogFormVisible: false,
      formLabelWidth: "100px",
      form: {},
      searchData: "",
      // typeList: [
      //   {
      //     name: "家",
      //     author: "老舍",
      //     price: "29.90"
      //   },
      //   {
      //     name: "凉生我们可不可以不忧伤",
      //     author: "乐米",
      //     price: "24.8"
      //   },
      //   {
      //     name: "未来",
      //     author: "王家",
      //     price: "28.8"
      //   },
      //   {
      //     name: "蔷薇",
      //     author: "0",
      //     price: "34.4"
      //   }
      // ],
      oldVal: [],
      newVal: [],
      editIndex: "",
      qubie: "",
      typeList: [],
    };
  },
  created () {
    this.getTypeList()
  },
  methods: {
      formatter(row, column) {
        return row.address;
      },
    async getTypeList () {
      const { data: res } = await this.$http.post('Room/RoomTypeInfo')
      if (res.code !== 200) {
        return this.$message.error('获取房间类型列表失败！')
      }
      this.typeList = res.data
       var i=0;
      for(;i<this.typeList.length;i++){
        if(this.typeList[i].room_price==0)
             this.typeList[i].room_price="该类型暂无房间";
      }
    },
     goAddPage () {
      this.$router.push('/roomAdd')
    },
        clearFilter() {
        this.$refs.myTable.clearFilter();
      },
    filterHandler(value, row, column) {
        const property = column['property'];
        return row[property] === value;
      },
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    },
    // 新增和编辑功能
   async add(roomExplain) 
    {
      // 编辑
      if (this.qubie == "edit") {
        // console.log(this.editIndex);
        var editType = this.typeList[this.editIndex];
        // console.log(editBook)
        editType.room_explain =  roomExplain;
  
        this.dialogFormVisible = false;
      const { data: res } = await this.$http.post("Room/RoomTypeModify?room_type="+editType.room_type+"&room_explain="+editType.room_explain);
     
       if (res.code!== 200) {
        return this.$message.error('修改房间类型失败！')
      }

        // this.roomType = "";
      }
      // 新增
      if (this.qubie == "add") {
        console.log(2);
        if (roomType == "" || roomExplain == "" || roomPrice == "") {
          this.dialogFormVisible = true;
          // console.log(1)
        } else {
          this.dialogFormVisible = false;
          // console.log(roomType, roomCondition, roomPrice);
          var newroom = {
            type: roomType,
            condition: roomCondition,
            price: roomPrice
          };
      
          this.typeList.push(newroom);
          this.roomType = "";
          this.roomCondition = "";
          this.roomPrice = "";
        }
      }
   
    },
    // 删除
    async dele(index,row) {
      console.log(index);
      console.log(row.room_type);
      const { data: res } = await this.$http.post("Room/RoomTypeDelete?room_type="+row.room_type);
      console.log(row.room_type);
      console.log(res);
       if (res.code!== 200) {
        return this.$message.error('删除房间类型失败！')
      }



      this.typeList.splice(index, 1);
    },
    // 点击编辑按钮
    edit(index, row) {
      // console.log(row.name);
      this.dialogFormVisible = true;
     
      this.roomExplain = row.room_explain;
     
      // console.log(index)
      this.editIndex = index;
      this.qubie = "edit";
    },
    // 点击添加按钮
    toAdd() {
      this.dialogFormVisible = true;
      this.roomType = "";
      this.roomCondition = "";
      this.roomPrice = "";
      this.qubie = "add";
    },
    // 查找
   
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="less">

</style>
