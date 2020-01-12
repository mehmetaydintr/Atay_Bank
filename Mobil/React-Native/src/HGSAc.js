import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import Header from './Header';
import {
  HeaderButtons,
  HeaderButton,
  Item,
} from 'react-navigation-header-buttons';
import React from 'react';
import axios from 'axios';
import CommonDataManager from './CommonDataManager';
import {BackHandler} from 'react-native';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  TextInput,
  StatusBar,
  Button,
  Alert,
  Image,
} from 'react-native';

import {
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';

import {StackActions} from 'react-navigation';
class HGSAc extends React.Component {
  state = {musteriNo: '', HesapNo: '', Bakiye: '', Tutar: ''};
  static navigationOptions = {
    drawerLabel: () => null,
  };
  componentWillMount() {
    let hesapNo = this.props.navigation.getParam('hesapNo', 0);
    let bakiye = this.props.navigation.getParam('bakiye', 0);
    this.setState({HesapNo: hesapNo});
    this.setState({Bakiye: bakiye});
    console.log(this.state.HesapNo);
    console.log(this.state.Bakiye);
  }
  HGSHesapAc() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    axios({
      method: 'post',
      url:
        'https://hgskurum20191228102003.azurewebsites.net/api/tutunamayanlar/hgskurum/yenihgs',
      data: {
        musteriNumarasi: musteriNo2,
        hgsBakiye: this.state.Tutar,
      },
    }).then(response => {
      console.log(response.status);
      this.setState({status: response.status});
      if (this.state.status == '200') {
        console.log(response.data);
        this.HGSHesapAcBanka();
      } else {
        Alert.alert(response.data);
      }
    });
  }
  HGSHesapAcBanka() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    axios({
      method: 'post',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/hgs/yenihgs',
      data: {
        HGSMusteriNumarasi: musteriNo2,
        hgsBakiyesi: this.state.Tutar,
      },
    }).then(response => {
      console.log(response.status);
      this.setState({status: response.status});
      if (this.state.status == '200') {
        console.log(response.data);
        this.setState({Bakiye: response.data});
        Alert.alert(response.data.toString());
        this.setState({Tutar: ''});
      } else {
        Alert.alert(response.data);
      }
    });
  }
  render() {
    let bakiye = this.props.navigation.getParam('bakiye', 0);
    const {
      ViewStyle,
      subViewstyle,
      TextStyle,
      buttonStyle,
      container,
      InputStyle,
      TextInputStyle,
    } = styles;
    return (
      <>
        <SafeAreaView>
          <Header headerText="HGS Ac" />
          <View>
            <View style={TextStyle}>
              <Text>25 ₺ HGS ücreti tutardan düşülecektir. </Text>
            </View>
            <View style={InputStyle}>
              <TextInput
                placeholder="Tutar"
                style={TextInputStyle}
                value={this.state.Tutar}
                onChangeText={text => this.setState({Tutar: text})}
                maxLength={18}
                keyboardType={'numeric'}
              />
            </View>
            <View style={buttonStyle}>
              <Button title="Ac" onPress={() => this.HGSHesapAc()} />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Drawer"
                onPress={() => this.props.navigation.toggleDrawer()}
              />
            </View>
          </View>
        </SafeAreaView>
      </>
    );
  }
}

const styles = StyleSheet.create({
  ViewStyle: {
    borderWidth: 1,
    borderRadius: 2,
    borderColor: '#ddd',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'center',
  },
  buttonStyle: {
    flexDirection: 'row',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'flex-end',
    position: 'relative',
  },
  subViewstyle: {
    borderWidth: 1,
    borderRadius: 20,
    borderColor: '#ddd',
    marginTop: 10,
    marginRight: 5,
    marginLeft: 5,
    padding: 5,
    justifyContent: 'flex-start',
    flexDirection: 'row',
    position: 'relative',
  },
  TextStyle: {
    marginTop: 20,
    color: 'black',
    fontSize: 30,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
    marginBottom: 20,
    marginLeft: 10,
  },
  container: {
    width: 150,
    height: 150,
    alignItems: 'center',
    justifyContent: 'center',
    marginLeft: 90,
    marginTop: 10,
    marginBottom: 20,
  },
  InputStyle: {
    borderBottomWidth: 1,
    marginRight: 5,
    marginLeft: 5,
    justifyContent: 'flex-start',
    flexDirection: 'row',
    position: 'relative',
  },
  TextInputStyle: {
    marginTop: 20,
    color: 'black',
    fontSize: 15,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
  },
});
export default HGSAc;
