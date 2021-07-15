<template>
  <div class="hello">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>设施系统</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="searchDiv">
      <el-col :span="18">
        <!-- 查询输入框 -->
        <el-input
          v-model="input"
          placeholder="请输入设施名或设施编号"
        ></el-input>
      </el-col>
      <el-button type="primary" icon="el-icon-search" @click="search(input)"
        >搜索</el-button
      >
      <!-- 模态框 -->
      <el-button type="primary" @click="toAdd">新添设施</el-button>

      <el-dialog title="添加设施" :visible.sync="dialogFormVisible">
        <el-form>
          <el-form-item label="设施编号" :label-width="formLabelWidth">
            <el-input v-model="facility_id"></el-input>
          </el-form-item>

          <el-form-item label="设施名称" :label-width="formLabelWidth">
            <el-input v-model="facility_name"></el-input>
          </el-form-item>

          <el-form-item label="设施价格" :label-width="formLabelWidth">
            <el-input v-model="facility_price"></el-input>
          </el-form-item>

          <el-form-item label="负责员工编号" :label-width="formLabelWidth">
            <el-input v-model="staff_id"></el-input>
          </el-form-item>
          <div v-for="item in room_num" :key="item">
            <el-form-item label="设施房间号" :label-width="formLabelWidth">
              <el-input v-model="room_id[item - 1]"></el-input>
            </el-form-item>
            <el-form-item label="设施状态" :label-width="formLabelWidth">
              <el-select
                v-model="facility_condition[item - 1]"
                placeholder="请选择"
              >
                <el-option
                  v-for="item in options"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </div>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button
            type="primary"
            @click="addroom(facility_id, facility_name, staff_id)"
            >添加房间</el-button
          >
          <el-button type="primary" @click="addfacility()">保存</el-button>
        </div>
      </el-dialog>

      <el-dialog
        title="修改设施状态"
        :visible.sync="dialogConditionVisible"
        width="300px"
      >
        <el-select v-model="label" placeholder="请选择">
          <el-option
            v-for="item in options"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          >
          </el-option>
        </el-select>
        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="editfacilitycondition()"
            >保存</el-button
          >
        </div>
      </el-dialog>
    </div>

    <div class="tableDiv">
      <el-table
        :data="FacilityData"
        style="width: 100%"
        ref="myTable"
        :default-sort="{ prop: 'facility_id', order: 'ascending' }"
      >
        // 子表格
        <el-table-column type="expand">
          <template slot-scope="scope">
            <el-table
              :data="scope.row.children"
              style="width: calc(100% - 47px)"
              class="two-list"
            >
              <el-table-column
                prop="room_id"
                label="拥有该设施的房间号"
                width="300"
              ></el-table-column>
              <el-table-column
                prop="facility_condition"
                label="该设施在该房间的状态"
                width="300"
              ></el-table-column>
              <el-table-column label="操作">
                <template slot-scope="condition_scope">
                  <i
                    class="el-icon-edit"
                    @click="toeditfacilitycondition(scope, condition_scope)"
                  ></i>
                </template>
              </el-table-column>
            </el-table>
          </template>
        </el-table-column>

        <el-table-column width="5"></el-table-column>
        <el-table-column
          prop="facility_id"
          label="设施编号"
          width="180"
        ></el-table-column>
        <el-table-column
          prop="facility_name"
          label="设施名称"
          width="180"
        ></el-table-column>
        <el-table-column
          prop="facility_price"
          label="设施价格"
          width="180"
        ></el-table-column>
        <el-table-column prop="staff_id" label="负责员工编号"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <i class="el-icon-close" @click="delefacility(scope)"></i>
            <i
              class="el-icon-edit"
              @click="editfacility(scope.row, scope.$index)"
            ></i>
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      room_num: 1,
      input: "",
      staff_id: "",
      facility_name: "",
      facility_id: "",
      facility_price: 0,
      room_id: [],
      facility_condition: [],
      //设施的dialog
      dialogFormVisible: false,
      //修改设施状态的dialog
      dialogConditionVisible: false,
      formLabelWidth: "100px",
      form: {},
      searchData: "",
      editIndex: -1,
      condition_facility_id: "",
      condition_room_id: "",
      label: "状态良好",
      options: [
        {
          label: "状态良好",
          value: "状态良好",
        },
        {
          label: "出现故障",
          value: "出现故障",
        },
      ],
      FacilityData: [
        // {
        //   facility_id: "0001",
        //   facility_name: "老舍",
        //   facility_price: 20,
        //   staff_id: "29.90",
        //   children: [
        //     { room_id: "222222", facility_condition: "1" },
        //     { room_id: "111222", facility_condition: "1" },
        //     { room_id: "224422", facility_condition: "1" }
        //   ]
        // },
        // {
        //   facility_id: "3123123",
        //   facility_name: "32",
        //   facility_price: 20,
        //   staff_id: "24.8",
        //   children: [
        //     { room_id: "222222" },
        //     { room_id: "111222" },
        //     { room_id: "224422" }
        //   ]
        // },
        // {
        //   facility_id: "未来",
        //   facility_name: "王家",
        //   facility_price: 20,
        //   staff_id: "28.8",
        //   children: [{ room_id: "222222" }, { room_id: "111222" }]
        // },
        // {
        //   facility_id: "蔷薇",
        //   facility_name: "安妮",
        //   facility_price: 20,
        //   staff_id: "34.4",
        //   children: [{ room_id: "222222" }]
        // }
      ],
      facilityqubie: "",
    };
  },
  created() {
    this.getFacilityData();
  },
  methods: {
    async getFacilityData() {
      const { data: res } = await this.$http.get("Facility");
      if (res.code !== 200) {
        return this.$message.error("获取设施列表失败！");
      }
      this.FacilityData = res.data;
      console.log(res);
      for (var i = 0; i < this.FacilityData.length; i++) {
        for (var j = 0; j < this.FacilityData[i].children.length; j++) {
          if (this.FacilityData[i].children[j].facility_condition == "1") {
            this.FacilityData[i].children[j].facility_condition = "状态良好";
          } else {
            this.FacilityData[i].children[j].facility_condition = "出现故障";
          }
        }
      }
    },

    //增加房间数
    addroom() {
      this.room_num++;
    },
    // 新增设施功能
    async addfacility() {
      // 编辑
      if (this.facilityqubie == "edit") {
        var editFacility = this.FacilityData[this.editIndex];
        editFacility.facility_id = this.facility_id;
        editFacility.facility_name = this.facility_name;
        editFacility.facility_price = parseInt(this.facility_price);
        editFacility.staff_id = this.staff_id;
        //清空children里的内容

        var length = editFacility.children.length;
        editFacility.children.splice(0, length);

        //赋予新值
        for (var i = 0; i < this.room_id.length; i++) {
          if (this.room_id[i] != "") {
            //非空时才添加
            var room_id = this.room_id[i];
            if (this.facility_condition[i] == "状态良好")
              facility_condition = true;
            else facility_condition = false;
            editFacility.children.push({ room_id, facility_condition });
          }
        }
        console.log(editFacility);
        //后端删除
        var temp_edit_facility_id = {
          facility_id: this.facility_id,
        };
        const { data: res } = await this.$http.post(
          "Facility/Del",
          temp_edit_facility_id
        );
        console.log(res);
        if (res.code !== 200) {
          return this.$message.error("删除设备失败");
        }
        //后端添加
        const { data: res_ } = await this.$http.post(
          "Facility/Add",
          editFacility
        );
        console.log(res_);
        if (res_.code !== 200) {
          return this.$message.error("增加设施失败！");
        }

        this.getFacilityData();

        this.dialogFormVisible = false;
        this.facility_id = "";
        this.facility_name = "";
        this.facility_price = 0;
        this.staff_id = "";
        //恢复room_id初值
        for (; this.room_id.length != 0; ) {
          this.room_id.pop();
        }
        //恢复facility_condition初值
        for (; this.facility_condition.length != 0; ) {
          this.facility_condition.pop();
        }
        this.room_num = 1;
        //console.log(this.room_id);
        this.facilityqubie = "";
      }
      // 新增
      if (this.facilityqubie == "add") {
        if (
          this.facility_id == "" ||
          this.facility_name == "" ||
          this.staff_id == "" ||
          this.room_id.length == 0 ||
          this.room_id.length != this.facility_condition.length ||
          this.facility_price <= 0
        ) {
          this.dialogFormVisible = true;
        } else {
          //console.log(this.facility_condition);
          this.dialogFormVisible = false;
          // console.log(facility_id, facility_name, staff_id);
          var newFacility = {
            facility_id: this.facility_id,
            facility_name: this.facility_name,
            facility_price: parseInt(this.facility_price), //强制属性转换
            staff_id: this.staff_id,
            children: [],
          };
          var room_id = "";
          var facility_condition = true;
          for (var i = 0; i < this.room_id.length; i++) {
            room_id = this.room_id[i];
            if (this.facility_condition[i] == "状态良好")
              facility_condition = true;
            else facility_condition = false;
            newFacility.children.push({ room_id, facility_condition });
          }

          //添加设施
          const { data: res } = await this.$http.post(
            "Facility/Add",
            newFacility
          );
          console.log(res);
          if (res.code !== 200) {
            return this.$message.error("增加设施失败！");
          }
          this.getFacilityData();
          this.facility_id = "";
          this.facility_name = "";
          this.facility_price = 0;
          this.staff_id = "";
          //恢复room_id初值
          for (; this.room_id.length != 0; ) {
            this.room_id.pop();
          }
          //恢复facility_condition初值
          for (; this.facility_condition.length != 0; ) {
            this.facility_condition.pop();
          }
          this.room_num = 1;
          //console.log(this.room_id);
          this.facilityqubie = "";
        }
      }
    },
    // 删除设备
    async delefacility(scope) {
      //后端
      var temp_facility_id = {
        facility_id: scope.row.facility_id,
      };
      const { data: res } = await this.$http.post(
        "Facility/Del",
        temp_facility_id
      );
      if (res.code !== 200) {
        return this.$message.error("删除设备失败");
      }
      this.getFacilityData();
    },
    // 点击编辑设备按钮
    editfacility(row, index) {
      this.dialogFormVisible = true;
      this.editIndex = index;
      this.facility_id = row.facility_id;
      this.facility_price = row.facility_price;
      this.facility_name = row.facility_name;
      this.staff_id = row.staff_id;
      this.room_num = row.children.length;
      for (var i = 0; i < this.room_num; i++) {
        this.room_id[i] = row.children[i].room_id;
        if (row.children[i].facility_condition == "1")
          this.facility_condition[i] = "状态良好";
        else this.facility_condition[i] = "出现故障";
      }
      console.log(this.facility_condition);
      this.facilityqubie = "edit";
    },
    // 点击添加设施按钮
    toAdd() {
      this.dialogFormVisible = true;
      this.facility_id = "";
      this.facility_name = "";
      this.facility_price = 0;
      this.staff_id = "";
      //恢复room_id初值
      for (; this.room_id.length != 0; ) {
        this.room_id.pop();
      }
      //恢复facility_condition初值
      for (; this.facility_condition.length != 0; ) {
        this.facility_condition.pop();
      }
      this.room_num = 1;
      this.facilityqubie = "add";
    },
    //点击编辑设施按钮
    toeditfacilitycondition(scope, condition_scope) {
      this.dialogConditionVisible = true;
      this.label = "设施良好";
      this.condition_facility_id = scope.row.facility_id;
      this.condition_room_id = condition_scope.row.room_id;
    },

    async editfacilitycondition() {
      var tempcondition = {
        facility_id: this.condition_facility_id,
        room_id: this.condition_room_id,
        facility_condition: true,
      };
      if (this.label == "设施良好") tempcondition.facility_condition = true;
      else tempcondition.facility_condition = false;

      // 后端
      const { data: res } = await this.$http.post(
        "Facility/AlertCondition",
        tempcondition
      );
      if (res.code !== 200) {
        return this.$message.error("修改房间设备状态失败");
      }
      this.condition_facility_id = "";
      this.condition_room_id = "";
      this.label = "设施良好";
      this.dialogConditionVisible = false;
      this.getFacilityData();
    },
    // 查找
    search(input) {
      // console.log(input)
      var newData = [];
      this.FacilityData.map((item, index) => {
        if (
          item.facility_id.includes(input) ||
          item.facility_name.includes(input) ||
          item.staff_id.includes(input)
        ) {
          newData.push(item);
          this.$refs.myTable.data = newData;
        }
      });
      return newData;
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="less"></style>
