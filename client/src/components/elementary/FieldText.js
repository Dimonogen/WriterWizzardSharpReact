import React, {Component} from 'react';
import {Form} from "react-bootstrap";

class FieldText extends Component {
    state = {
      value: "",
        useProp: false
    };

    constructor(props) {
        super(props);
        if(this.props.useProp)
            this.state.useProp = true;
        if(this.props.value != null)
            this.state.value = this.props.value;
        else
            this.state.value = "";
    }

    handleChange = (valueN) => {
        this.setState({
            value: valueN
        });
        this.props.setValue(valueN, this.props.id);
    };



    render() {
        return (
            <div>
                <Form.Group className="mb-3 w-100">
                    <Form.Label style={{textTransform: "capitalize"}}>{this.props.name}</Form.Label>
                    {this.props.value == null?
                        <span className="float-end">{"0/" + this.props.maxlen}</span>
                        :
                        this.props.value.length > this.props.maxlen?
                            <span className="float-end text-danger">Превышена максимальная длина {
                                " " + this.props.value.length + "/" + this.props.maxlen}</span>
                            :
                        this.props.value.length === 0 && this.props.nullable === false?
                            <span className="float-end text-danger">Поле не заполнено
                                {" " + this.props.value.length + "/" + this.props.maxlen}</span>
                            :
                        this.props.value.length < this.props.minlen?
                            <span className="float-end text-danger">Слишком коротко {
                                " " + this.props.value.length + "/" + this.props.maxlen}</span>
                            :
                        <span className="float-end">{this.props.value.length + "/" + this.props.maxlen}</span>
                    }
                    <Form.Control
                        value={this.state.useProp || this.props.disabled? this.props.value:this.state.value}
                        as={this.props.rows != null?'textarea':"input"}
                        rows={this.props.rows}
                        className={this.props.value == null? "": this.props.value.length > this.props.maxlen?"text-danger":""}
                        onChange={e => this.handleChange(e.target.value)}
                        placeholder={this.props.placeholder}
                        disabled={this.props.disabled}
                    />
                </Form.Group>
            </div>
        );
    }
}

export default FieldText;